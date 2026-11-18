# BetterDebug

<xref:Espale.Utilities.BetterDebug> has the same methods as `Debug.Log`, `Debug.LogWarning`, `Debug.LogError`, `Debug.DrawRay` and `Debug.DrawLine`, but the calls are wrapped in `#if UNITY_EDITOR`, so they do nothing in builds.

```csharp
BetterDebug.Log("Spawned wave " + wave, this);
BetterDebug.DrawRay(transform.position, velocity, Color.cyan);
```

`DrawRay` and `DrawLine` draw in green by default and last one frame at 60 FPS (`Constants.DT_60FPS`) unless you pass a duration.

> [!TIP]
> Use the regular `Debug` methods for anything you want to see in a development build's log.
