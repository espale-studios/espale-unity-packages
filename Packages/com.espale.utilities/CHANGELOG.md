# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [1.0.4] - 2024-12-19

### Removed

- Removed auto prefab instantiating system from `Singleton` as it was not functioning properly.

## [1.0.3] - 2024-05-08

### Added
- Added `MathUtilities.BezierInterpolation` method for interpolating between two ends of a [Bézier curve](https://en.wikipedia.org/wiki/B%C3%A9zier_curve).

## [1.0.2] - 2024-04-29

### Fixed
- Fixed a stack overflow issue caused by `MathUtilities.Direction` `Vector2` override introduced in the last patch.

## [1.0.1] - 2024-04-28

### Added
- Added `ClampVector`, `RoundVector` `MinVector` and `MaxVector` methods to the `MathUtilities` class.
- Added `BetterDebug.Ray` and `BetterDebug.Line` methods.

### Changed
- `MathUtilities.Direction` now has a `Vector2` override.
- `RandomUtilities.RandomVector2`, `RandomUtilities.RandomVector3` & `RandomUtilities.RandomVector4` methods now work correctly and the range is now changeable.

### Removed
- Removed `Vector4` overrides for utility methods.

## [1.0.0] - 2024-04-20

The first version got released.
