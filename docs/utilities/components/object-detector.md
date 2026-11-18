# ObjectDetector

<xref:Espale.Utilities.Components.ObjectDetector> (`Espale.Utilities.Components`) checks every frame whether a target `Transform` is inside a circle or rectangle around this GameObject, and invokes events when it enters and leaves. It doesn't need colliders or physics.

| Field | Description |
| --- | --- |
| **Shape** | `Circle` or `Rectangle`. |
| **Target** | The `Transform` to watch, for example the player. |
| **On Enter**, **On Exit** | `UnityEvent`s invoked when the target enters or leaves the shape. |
| **Radius** | Circle size (used when Shape is Circle). |
| **Width**, **Height** | Rectangle size, centered on the GameObject (used when Shape is Rectangle). |

The shape is drawn in blue in the Scene view. The rectangle check only uses the x and y axes, while the circle check uses the full 3D distance.
