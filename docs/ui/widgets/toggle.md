# Toggle

<xref:Espale.UI.Toggle> is an on/off control with an animated fill. It's a `Selectable`, so it works with mouse, touch, keyboard and gamepad navigation.

| Field | Description |
| --- | --- |
| **Is On** | The current state. |
| **On Toggle** | `UnityEvent` invoked whenever the state changes. |
| **Fill Transition Speed** | How fast the fill animates (1 to 20). |
| **Fill Color** | Color of the fill when the toggle is on. |
| **Anim Type** | How the fill appears: `Horizontal`, `Vertical`, `Radial90`, `Radial180`, `Radial360`, `Fade` or `None`. |
| **Fill Scale** | Size of the fill relative to the box (0 to 1). |
| **Mask**, **Fill** | The child images used for the fill. The prefab sets these up. |

```csharp
// Read the state when it changes
toggle.onToggle.AddListener(() => musicEnabled = toggle.isOn);

// Set the state without the fill animation, for example when loading settings
toggle.SetIsOnStateInstantly(savedValue);
```

`SetIsOnStateInstantly` invokes **On Toggle** only if the state actually changes.

> [!TIP]
> This class is `Espale.UI.Toggle`. If a script also uses `UnityEngine.UI`, write the full name or add `using Toggle = Espale.UI.Toggle;` to avoid a clash with Unity's own `Toggle`.
