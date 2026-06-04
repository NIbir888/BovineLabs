# Plan: Refactor BlendTree2DClip to Use EntityLink Key Pattern (Proper Fix)

## Goal
Refactor `BlendTree2DClip` to bake the entity link key instead of resolving the linked component at bake time, matching the pattern used by every other clip in the codebase.

## Root Cause
`BlendTree2DClip` is the **only clip** that uses `context.TryResolveLinkComponent<T>()` — a bake-time hierarchy walk via `GetComponentInParent<EntityLinkRootAuthoring>()` from the Animator binding. This fails when `EntityLinkRootAuthoring` isn't in the Animator's parent chain.

All other clips (EntityLinkCopyTransformClip, PhysicsVelocityClip, PhysicsForceClip, etc.) use `EntityLinkAuthoringUtility.TryGetKey()` to bake a `ushort` key, then resolve at runtime via `EntityLinkResolver`.

## Changes Required

### 1. `BlendTree2DDirectionClipData` (Data layer)
**File:** `Packages/BovineLabs.Timeline.Animation/BovineLabs.Timeline.Animation.Data/BlendTree2DMotionData.cs`

Replace `Entity ReadEntity` with `ushort ReadLinkKey`:

```csharp
public struct BlendTree2DDirectionClipData : IAnimatedComponent<float2>
{
    public BlendDirectionReadKind ReadKind;
    public ushort ReadLinkKey;          // was: Entity ReadEntity
    [CreateProperty] public float2 Value { get; set; }
    public float ClipIn;
    public float TimeScale;
    public float3 PositionOffset;
    public quaternion RotationOffset;
    public bool RemoveStartOffset;
    public bool ApplyFootIK;
}
```

### 2. `BlendTree2DClip` (Authoring layer)
**File:** `Packages/BovineLabs.Timeline.Animation/BovineLabs.Timeline.Animation.Authoring/BlendTree2DClip.cs`

- Remove `TryGetReadEntity` and `TryGetLinkedComponent<T>` methods entirely
- In `Bake()`, use `EntityLinkAuthoringUtility.TryGetKey()` to extract the key
- Remove `using Unity.Physics.Authoring` (no longer needed)

```csharp
public override void Bake(Entity clipEntity, BakingContext context)
{
    ushort readLinkKey = 0;
    if (ReadKind != BlendDirectionReadKind.ClipValue)
    {
        if (ReadFrom == null)
        {
            Debug.LogError($"{nameof(BlendTree2DClip)} '{name}' needs {nameof(ReadFrom)}.");
            return;
        }
        if (!EntityLinkAuthoringUtility.TryGetKey(ReadFrom, out var key))
        {
            Debug.LogError($"{nameof(BlendTree2DClip)} '{name}' could not resolve key for '{ReadFrom.name}'.");
            return;
        }
        readLinkKey = key;
    }

    context.Baker.AddComponent(clipEntity, new BlendTree2DDirectionClipData
    {
        Value = BlendParameter,
        ReadKind = ReadKind,
        ReadLinkKey = readLinkKey,
        ClipIn = (float)context.Clip.clipIn,
        TimeScale = (float)context.Clip.timeScale,
        PositionOffset = positionOffset,
        RotationOffset = Quaternion.Euler(eulerAnglesOffset),
        RemoveStartOffset = removeStartOffset,
        ApplyFootIK = applyFootIK
    });

    base.Bake(clipEntity, context);
}
```

### 3. `UpdateDynamicBlendParametersJob` (Runtime system)
**File:** `Packages/BovineLabs.Timeline.Animation/BovineLabs.Timeline.Animation/TimelineAnimationBlendTree2DTrackSystem.cs`

The runtime job currently does `PhysicsVelocityLookup.TryGetComponent(clipData.ReadEntity, out var pv)`. It needs to:
1. Resolve the entity link key at runtime via `EntityLinkResolver`
2. Then look up the component on the resolved entity

The job needs access to `EntityLinkResolver` dependencies:
- `UnsafeComponentLookup<EntityLinkSource>`
- `UnsafeBufferLookup<EntityLinkEntry>`

**Key question:** The `UpdateDynamicBlendParametersJob` runs on clip entities. To resolve the link, it needs a "root" entity to start from. The clip entity has `TrackBinding` → track binding entity. From the track binding entity, it can call `EntityLinkResolver.TryResolve(bindingEntity, key, sources, entries, out var resolvedEntity)`.

Updated job:
```csharp
[BurstCompile]
[WithAll(typeof(ClipActive))]
private partial struct UpdateDynamicBlendParametersJob : IJobEntity
{
    [ReadOnly] public ComponentLookup<PhysicsVelocity> PhysicsVelocityLookup;
    [ReadOnly] public ComponentLookup<PlayerMoveInput> PlayerMoveInputLookup;
    [ReadOnly] public UnsafeComponentLookup<EntityLinkSource> EntityLinkSourceLookup;
    [ReadOnly] public UnsafeBufferLookup<EntityLinkEntry> EntityLinkEntryLookup;
    [ReadOnly] public ComponentLookup<TrackBinding> TrackBindingLookup;

    private void Execute(Entity clipEntity, ref BlendTree2DDirectionClipData clipData)
    {
        if (clipData.ReadKind == BlendDirectionReadKind.ClipValue)
            return;

        if (clipData.ReadLinkKey == 0)
        {
            clipData.Value = float2.zero;
            return;
        }

        // Resolve the link at runtime
        if (!TrackBindingLookup.TryGetComponent(clipEntity, out var binding) ||
            !EntityLinkResolver.TryResolve(binding.Value, clipData.ReadLinkKey,
                EntityLinkSourceLookup, EntityLinkEntryLookup, out var resolvedEntity))
        {
            clipData.Value = float2.zero;
            return;
        }

        if (clipData.ReadKind == BlendDirectionReadKind.PhysicsLinearVelocityNormalized)
        {
            if (PhysicsVelocityLookup.TryGetComponent(resolvedEntity, out var pv))
            {
                var vel2d = new float2(pv.Linear.x, pv.Linear.z);
                var lengthSq = math.lengthsq(vel2d);
                clipData.Value = lengthSq > DirectionEpsilon
                    ? vel2d / math.sqrt(lengthSq)
                    : float2.zero;
            }
            else
            {
                clipData.Value = float2.zero;
            }
        }
        else if (clipData.ReadKind == BlendDirectionReadKind.PlayerMoveInput)
        {
            if (PlayerMoveInputLookup.TryGetComponent(resolvedEntity, out var moveInput))
            {
                var vel2d = moveInput.Value;
                var lengthSq = math.lengthsq(vel2d);
                clipData.Value = lengthSq > 1f
                    ? vel2d / math.sqrt(lengthSq)
                    : vel2d;
            }
            else
            {
                clipData.Value = float2.zero;
            }
        }
    }
}
```

And in `OnUpdate`, add the lookups:
```csharp
state.Dependency = new UpdateDynamicBlendParametersJob
{
    PhysicsVelocityLookup = SystemAPI.GetComponentLookup<PhysicsVelocity>(true),
    PlayerMoveInputLookup = SystemAPI.GetComponentLookup<PlayerMoveInput>(true),
    EntityLinkSourceLookup = state.GetUnsafeComponentLookup<EntityLinkSource>(true),
    EntityLinkEntryLookup = state.GetUnsafeBufferLookup<EntityLinkEntry>(true),
    TrackBindingLookup = SystemAPI.GetComponentLookup<TrackBinding>(true)
}.ScheduleParallel(state.Dependency);
```

### 4. ASMDEF updates
**`BovineLabs.Timeline.Animation.asmdef`** (runtime) — needs to add:
- `"BovineLabs.Timeline.EntityLinks.Data"` (for `EntityLinkSource`, `EntityLinkEntry`)
- `"BovineLabs.Timeline.EntityLinks"` (for `EntityLinkResolver`)

**`BovineLabs.Timeline.Animation.Authoring.asmdef`** — already has `"BovineLabs.Timeline.EntityLinks.Authoring"`, no change needed.

### 5. Test updates
**File:** `Packages/BovineLabs.Timeline.Animation/BovineLabs.Timeline.Animation.Tests/AnimationDataTests.cs`

Update `BlendTree2DDirectionClipDataTests`:
- `Default_ZeroFields`: assert `ReadLinkKey == 0` instead of `ReadEntity == Entity.Null`
- `Fields_SetCorrectly`: use `ReadLinkKey = 42` instead of `ReadEntity = entity`

## Files to Change (summary)
1. `Packages/BovineLabs.Timeline.Animation/BovineLabs.Timeline.Animation.Data/BlendTree2DMotionData.cs` — data struct
2. `Packages/BovineLabs.Timeline.Animation/BovineLabs.Timeline.Animation.Authoring/BlendTree2DClip.cs` — authoring
3. `Packages/BovineLabs.Timeline.Animation/BovineLabs.Timeline.Animation/TimelineAnimationBlendTree2DTrackSystem.cs` — runtime
4. `Packages/BovineLabs.Timeline.Animation/BovineLabs.Timeline.Animation/BovineLabs.Timeline.Animation.asmdef` — add entity links refs
5. `Packages/BovineLabs.Timeline.Animation/BovineLabs.Timeline.Animation.Tests/AnimationDataTests.cs` — test data assertions

## Risks
- **Structural change to `BlendTree2DDirectionClipData`**: changes `Entity` field to `ushort`. Any existing baked data will be invalid — requires re-bake. This is acceptable for development.
- **New asmdef dependency**: `BovineLabs.Timeline.Animation` → `BovineLabs.Timeline.EntityLinks` + `.Data`. Need to verify no circular dependency exists. Confirmed safe: EntityLinks does not reference Animation.
- **Runtime lookup path change**: the clip entity needs `TrackBinding` to find the root entity for link resolution. Verified: `TrackBinding` is already present on clip entities (used in `GatherClipDataJob`).

## Verification
```bash
unity-cli exec "UnityEditor.AssetDatabase.Refresh(UnityEditor.ImportAssetOptions.ForceSynchronousImport); return \"REFRESHED\";" 2>&1
# Wait 15-25s
unity-cli console --filter error 2>&1
```
- Should show NO COMPILATION ERRORS
- The "could not resolve Movement Body Link" errors should be gone (bake no longer tries to resolve)
- At runtime, link resolution happens via EntityLinkResolver against the EntityLinkEntry buffer
