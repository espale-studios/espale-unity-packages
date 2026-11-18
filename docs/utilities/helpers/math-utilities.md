# MathUtilities

<xref:Espale.Utilities.MathUtilities> collects the small math operations that come up again and again in gameplay code.

## Ranges and remapping

```csharp
// Exclusive range: the end points are not included. The bounds can be in any order.
MathUtilities.IsInRange(5f, 10f, 0f);        // true
MathUtilities.IsInRange(10f, 0f, 10f);       // false

// Closed range: the end points are included.
MathUtilities.IsInClosedRange(10f, 0f, 10f); // true

// Remap a value from [0, 1] to [-1, 1]. The value is clamped to the source range first.
MathUtilities.Remap(0.75f, 0f, 1f, -1f, 1f);          // 0.5
MathUtilities.Remap(2f, 0f, 1f, -1f, 1f);             // 1 (clamped)

// Same, without clamping.
MathUtilities.RemapUnclamped(2f, 0f, 1f, -1f, 1f);    // 3
```

Both remap methods return `0` when either range has zero length.

## Means

```csharp
MathUtilities.Mean(new[] { 1, 2, 3, 4 });     // 2.5
MathUtilities.Mean(new List<float>());        // 0 for an empty collection
```

## Vectors

Each of these has a `Vector2` and a `Vector3` overload and works on every axis:

| Method | Result |
| --- | --- |
| `AbsVector(v)` | Absolute value of each axis. |
| `ClampVector(v, min, max)` | Each axis clamped between `min` and `max`. |
| `RoundVector(v)` | Each axis rounded to the nearest whole number. |
| `MinVector(v, val)` | `Mathf.Min(axis, val)` on each axis. |
| `MaxVector(v, val)` | `Mathf.Max(axis, val)` on each axis. |
| `Direction(from, to, normalized = true)` | The vector from `from` to `to`, normalized unless you pass `false`. |

```csharp
var toPlayer = MathUtilities.Direction(enemy.position, player.position);
var offset = MathUtilities.Direction(a, b, normalized: false); // same as b - a
```

`RotateVector2(vector, angle)` rotates a `Vector2` by an angle in **radians**.

## Circles and angles

```csharp
// A point on a circle. UnitCirclePosRadians takes radians, UnitCirclePosDegrees takes degrees.
Vector2 p = MathUtilities.UnitCirclePosDegrees(90f, r: 2f);   // (0, 2)

// Clamp an angle in degrees, handling wrap-around.
float clamped = MathUtilities.ClampAngle(angle, -45f, 45f);

// Normalize to [-180, 180) first (or your own range), then clamp.
float clampedNormalized = MathUtilities.ClampAngleNormalized(350f, -45f, 45f); // -10
```

## Bézier interpolation

`BezierInterpolation` returns the point at `t` on a Bézier curve from `p1` to `p2`, with any number of control points in between. `t` is clamped to `[0, 1]`. With no control points it is a plain lerp.

```csharp
var controls = new[] { new Vector2(0f, 2f), new Vector2(2f, 2f) };
Vector2 point = MathUtilities.BezierInterpolation(start, end, t, controls);
```
