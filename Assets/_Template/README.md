# BovineLabs Template

Reusable Unity template assets for BovineLabs projects.

## Install

In Unity Package Manager, choose **Add package from git URL...** and use:

```text
https://github.com/NIbir888/BovineLabs.git?path=/Assets/_Template#Hit-Detection
```

If you want the latest pushed commit on the branch, omit `#Hit-Detection` only if your project should track the default branch.

## Contents

- `Prefab/Player/Player_XX.prefab`

## Notes

This package depends on Unity Entities/Physics and BovineLabs packages used by the prefab. If Unity does not resolve Git dependencies automatically in your project version, add the dependency URLs from `package.json` to your project's `Packages/manifest.json`.
