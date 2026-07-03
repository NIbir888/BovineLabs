# Shadow1E Level Assets Fix

Missing dependency assets required by `Shadow1E V1.unity`.

These assets resolve in the original `Vex-Combat-Demo3` project but were missing
in the BovineLabs project, causing missing-prefab / null-material errors.

## Contents

| Folder | Asset | GUID |
|---|---|---|
| `NeophyteV3/` | `NeoV3.prefab` (+ textures, materials, rig, dagger) | `9a74d378f902e77438123c37b6328a33` |
| `FlameKnight/` | `FlameKnight.controller` (+ rig, animations, sword) | `7f963bc7fdc59b4488e51e21f4ed2920` |
| `Settings/` | `SampleSceneProfile.asset` (Volume profile) | `a6560a915ef98420e9faacc1c7438823` |
| `HovlMaterials/` | `Smoke12cg.mat` (Wave/Plane material) | `702a250bb24700c4fb4a74b638bf5f2f` |
| `HovlMaterials/` | `Waves21cg2.mat` (Plane.13 material) | `8a2d576b47babc843a00f97e63d4ef51` |

All original `.meta` files are preserved so the GUIDs match the scene references.

## Licensing note

`HovlMaterials/` contains assets from the third-party **Hovl Studio** VFX pack.
Keep this repository private unless you hold the appropriate license to redistribute them.

## Install

Add to `Packages/manifest.json`:

```json
"com.vex.shadow1elevelfix": "https://github.com/NIbir888/Shadow1E-level-assets-Fix.git"
```
