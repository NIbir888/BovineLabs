# VEX Boss Room 1

Self-contained Unity package containing the **Boss Room 1** scene and every
asset dependency it references, so the scene loads with **no additional
packages** required.

## What is included

- `Scene/Boss Room 1.unity` (+ meta)
- `Dependencies/...` — every referenced asset grouped by source:
  - `shadow1/` — wall, pillar, pit, boss-room meshes/prefabs/materials/lighting
  - `bonecrusher/` — BoneCrusher statue rig + materials + textures
  - `srrubfishvfx03/` — fire VFX materials + textures
  - `pilotostudio1/` — Vortex swirled-dot sprite texture
  - `mk/` — MK/Toon shader files used by some boss materials
  - `com.agni.level/SampleScene/LightingData.asset` — baked lighting data
  - `Assets/...` — Rubfish VFX textures/shaders/meshes, FireLevel/1.mat, Shadow1EFix SampleSceneProfile

All `.meta` files are preserved so the original GUIDs match the scene references.

## What is NOT included (Unity-shipped, always present)

The following are intentionally NOT bundled because Unity ships them in every
URP / core install:

- `Packages/com.unity.render-pipelines.universal/...`
- `Packages/com.unity.render-pipelines.core/...`
- `Packages/com.unity.shadergraph/...` editor scripts
- `Library/unity default resources`

## Install

Add to `Packages/manifest.json`:

```json
"com.vex.bossroom1": "https://github.com/NIbir888/Boss-Room-1-Level.git"
```

## Known issue

`SampleScene/LightingData.asset` (from the original Shadow1E V1 package) fails
to load with an "Unknown error" — this issue exists in the source package too
and is not introduced by this package.
