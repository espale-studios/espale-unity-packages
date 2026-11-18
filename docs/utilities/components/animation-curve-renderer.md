# AnimationCurveRenderer

<xref:Espale.Utilities.Components.AnimationCurveRenderer> (`Espale.Utilities.Components`) draws an `AnimationCurve` as a line between two points using a `LineRenderer`. The curve's value pushes the line sideways, which is handy for lightning, ropes, laser beams or wobbly lines.

It requires a `LineRenderer` on the same GameObject.

| Field | Description |
| --- | --- |
| **Start Position**, **End Position** | The two ends of the line. Relative to the GameObject's position unless **Use Global Position** is on. |
| **Use Global Position** | Treat the positions as world positions. |
| **Curve** | The offset along the line. The x axis goes from the start (0) to the end (1). The y value moves the line perpendicular to it. |
| **Magnitude** | Multiplies the curve's value. |
| **Auto Draw On Update** | Redraw the line every frame. Turn it off and call `DrawCurve()` yourself if the line rarely changes. |
| **T** | How much of the line to draw, from 0 (nothing) to 1 (the whole line). Animate it to make the line grow. |
| **Resolution** | Number of segments in the line (at least 2). |

```csharp
// Grow a beam over half a second
IEnumerator GrowBeam(AnimationCurveRenderer beam, float duration = 0.5f)
{
    for (var time = 0f; time < duration; time += Time.deltaTime)
    {
        beam.t = time / duration;
        yield return null;
    }
    beam.t = 1f;
}
```

`GetLinePosAt(t)` returns the world position of the line at any point between 0 and 1, which you can use to place effects along it.
