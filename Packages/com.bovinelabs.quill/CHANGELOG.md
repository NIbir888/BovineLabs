# Changelog

## [1.2.6] - 2026-05-09

### Fixed
* Timed-duration draws no longer depend on rewound frame allocators.
* GlobalDraw now waits on tracked world dependencies before reading cross-world draw streams.
* Cylinder and capsule side counts now use the requested side count without the debug multiplier.
* Release builds expose the DrawSystem singleton CameraCulling API and can clean up rendering objects without URP.
* URP render graph draws now declare global state modification for all camera types.
* Physics debug capsule and terrain collider drawing now handle scaled capsules, partial terrain chunks, and terrain culling bounds correctly.
* Drawer validation paths avoid managed assertion strings in Burst-callable APIs.
* Draw toolbar enabled state is saved between sessions.

## [1.2.5] - 2026-04-04

### Fixed
* 6.6.0a2+ support

## [1.2.4] - 2026-03-28

### Fixed
* Physics toolbar compatibility with anchor

## [1.2.3] - 2026-03-13

### Changed
* Now depends on Core 1.6.0
* If using Anchor, now depends on Anchor 2.0.0

### Fixed
* Toolbar systems remembering state

## [1.2.2] - 2025-01-15

### Fixed
* Collections 2.6.4 and 6.5.0a5

## [1.2.1] - 2025-12-06

### Added
* SolidTriangles overload that takes an array of colours
* BidirectionalArrow
* AABB
* U6.4+ URP support
* Switching culling camera automatically if Scene View is focused

## [1.2.0] - 2025-10-02

### Added
* A bunch of config variables for customizing how default drawers work
* DrawSystem singleton now has CameraCulling property you can pass to jobs for frustum culling

### Changed
* Added frustum culling to physics drawing
* Switched to use SparseUploader

### Fixed
* Buffer inflight errors

### Removed
* Internal frustum culling as it rarely helped; instead I've exposed the data so you can easily apply frustum culling to your own custom draw systems

## [1.1.2] - 2025-08-17

### Added
* Support for APP_UI_EDITOR_ONLY
* QuillSettings with the ability to filter what cameras render

### Fixed
* Physics drawer capsule rotation

## [1.1.1] - 2025-04-26

### Changed
* Updated Anchor support to 1.2.0

### Documentation
* Clarified Circle drawer documentation

## [1.1.0] - 2025-03-29

### Changed
* Updated to Anchor 1.1.1
* Updated to Core 1.4.1 (requires entities 1.4.X)

### Fixed
* Improved a lot of safety in the back end for users
* Compile errors if Unity.Physics is missing
* Modifying in flight GraphicsBuffers
* Shaders broken in URP builds
* Missing conditionals on Text variations
* Release builds

## [1.0.2] - 2025-03-14

### Changed
* If using Anchor, now depends on 1.1.0
* Updated toolbar with new Anchor features
    * Reduced allocations
    * State is now saved between sessions
* Updated shaders to HLSL
* Text shader reworked

### Fixed
* Incorrectly sharing GraphicsBuffer between multiple cameras 

## [1.0.1] - 2025-03-09

### Added
* DrawEditor which provides the Update event for a simple way to draw in editor without having to create your own FrameUtility.

### Fixed
* A cleanup error that could occur when using URP.

### Documentation
* DrawEditor documentation and sample.
* Added Configuration section to read me.

## [1.0.0] - 2025-03-03

### Added
* Initial release.
