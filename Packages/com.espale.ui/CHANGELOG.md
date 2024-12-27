# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [1.0.4] - 2024-12-27

### Added

- `UILayer` now has `public` methods for both hiding and focusing on itself (`HideIfFocused()`, `Hide()`, `Focus()`).

## [1.0.3] - 2024-12-25

### Changed

- `UILayer`'s will now auto-disable when a scene starts, if it's not the default active layer.
- Changed the `canvasGroup` and `canvas` variables from `private` to `protected` to make implementing custom UI Layers easier.

## [1.0.2] - 2024-12-20

### Added
- Added `UILayerManager.HideLayer(UILayer)` method to specify the layer to hide.
- Added `UILayerManager.ToggleLayer(UILayer)` method to toggle a layers visibility.
- Added a boolean field called `defaultActiveLayer` to `UILayer`, which specifies whether the layer blocks the layers below it, in terms of interactivity.

### Changed
- Renamed `UILayerManager.HideLayer()` to `UILayerManager.HideTopLayer()` to be more clear.
- `UILayerManager` is no longer a singleton, it's a static class now. Yet, it still inherits from `MonoBehaviour` so that its methods can be called from Prefabs with the component.


## [1.0.1] - 2024-12-19

### Changed
- Updated `FPSDisplayer` to properly work with the updated version of Espale.Utilities (v1.0.4)


## [1.0.0] - 2024-04-20

The first version got released.
