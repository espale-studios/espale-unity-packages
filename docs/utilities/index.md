# Utilities

`com.espale.utilities` is a collection of small, general-purpose tools for 2D game development: static helper classes for math, randomness, colors, strings, audio, input and debugging, a simple save system, a generic singleton and a handful of ready-made components.

It is the base package: [Localization](../localization/index.md) and [UI](../ui/index.md) both depend on it.

## Install

```console
openupm add com.espale.utilities
```

**Dependencies:** Input System `1.3.0`, URP `14.0.0`, [NaughtyAttributes](https://github.com/dbrizov/NaughtyAttributes) `2.1.4`. See [Getting started](../getting-started.md) for other ways to install.

## What's inside

### Static helpers (`Espale.Utilities`)

| Class | What it does |
| --- | --- |
| <xref:Espale.Utilities.MathUtilities> | Means, ranges, remapping, vector clamping and rounding, directions, Bézier interpolation, angle clamping. |
| <xref:Espale.Utilities.RandomUtilities> | Random lerps, chance rolls, random unit vectors and arrays, picking and shuffling. |
| <xref:Espale.Utilities.CollectionUtilities> | Removing duplicates and picking a random item from any `IEnumerable`. |
| <xref:Espale.Utilities.ColorUtilities> | Extra named colors, brightening, darkening, grayscale and per-channel replacement. |
| <xref:Espale.Utilities.StringUtilities> | Word wrapping and scientific notation with rich text `sup` tags. |
| <xref:Espale.Utilities.AudioUtilities> | Linear volume to decibel conversion and random clip picking. |
| <xref:Espale.Utilities.InputUtilities> | Detecting the connected gamepad and its platform (Xbox, PlayStation, Nintendo). |
| <xref:Espale.Utilities.GizmoUtilities> | Drawing arrows and crosses as gizmos. |
| <xref:Espale.Utilities.BetterDebug> | `Debug.Log`, `DrawRay` and `DrawLine` wrappers that only run in the editor. |
| <xref:Espale.Utilities.Constants> | Pixel sizes for 16 and 32 PPU, `1/√2`, gravity and fixed frame delta times. |

See [Helper classes](helpers/index.md) for examples.

### Systems

| Type | What it does |
| --- | --- |
| <xref:Espale.Utilities.SaveUtilities> | Saves and loads serializable values to files under `Application.persistentDataPath`. See [Saving values](save-utilities.md). |
| <xref:Espale.Utilities.Singleton`1> | A `MonoBehaviour` singleton base class that survives scene loads. See [Singleton](singleton.md). |

### Components

| Component | Namespace | What it does |
| --- | --- | --- |
| <xref:Espale.Utilities.Components.AnimationCurveRenderer> | `Espale.Utilities.Components` | Draws an `AnimationCurve` between two points with a `LineRenderer`. |
| <xref:Espale.Utilities.Components.ObjectDetector> | `Espale.Utilities.Components` | Fires events when a target enters or leaves a circle or rectangle. |
| <xref:Espale.Utilities.Components.Light2DSpriteMatcher> | `Espale.Utilities.Components` | Keeps a sprite `Light2D`'s cookie sprite in sync with a `SpriteRenderer`. |
| <xref:Espale.Utilities.Components.Colliders.ArcCollider2D> | `Espale.Utilities.Components.Colliders` | Builds an arc-shaped `EdgeCollider2D`. |
| <xref:Espale.Utilities.Components.Randomizers.AudioSourceRandomizer> | `Espale.Utilities.Components.Randomizers` | Randomizes an `AudioSource`'s clip, volume and pitch. |
| <xref:Espale.Utilities.Components.Randomizers.SpriteRendererRandomizer> | `Espale.Utilities.Components.Randomizers` | Randomizes a `SpriteRenderer`'s sprite and color. |

See [Components](components/index.md) for setup details.

### Editor

`EditorScriptingUtilities` has small IMGUI helpers for custom inspectors and windows. See [Editor helpers](editor-scripting-utilities.md).
