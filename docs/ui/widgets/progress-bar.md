# ProgressBar

<xref:Espale.UI.ProgressBar> shows a value between a minimum and a maximum as a filling bar. Create a **Linear Progress Bar** for a horizontal bar, or a **Radial Progress Bar** for a ring with an optional icon in the middle.

| Field | Description |
| --- | --- |
| **Has Boundries** | Clamp **Current** between **Min** and **Max**. |
| **Max Value**, **Minimum Value**, **Current Value** | The range and the value to show. In the Inspector, Max goes up to 1000 and the other two are limited to Max. |
| **Instant Fill** | Jump straight to the new value instead of animating. |
| **Filling Speed** | How fast the bar and colors move toward their targets (1 to 20). Hidden when **Instant Fill** is on. |
| **Background Color**, **Filler Color** | Colors of the bar. Changing them fades smoothly. |
| **Is Radial** | Use the radial layout. Must match the prefab you started from. The fields below only appear when it's on. |
| **Outer Circle Radius** | Roundness of the radial bar's outer mask. |
| **Inner Circle Radius** | Size of the inner circle (0 to 1), which sets the ring's thickness. |
| **Inner Circle Color**, **Inner Circle Icon**, **Inner Circle Icon Color** | The inner circle and the icon shown in it. |

```csharp
healthBar.max = player.maxHealth;   // fields in code: min, max, current
healthBar.current = player.health;    // the bar animates toward the new value

float percent = healthBar.GetPercentage();     // e.g. 72.5
```

The bar animates with unscaled time, so it keeps moving while the game is paused.
