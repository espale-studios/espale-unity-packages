# ColorUtilities

<xref:Espale.Utilities.ColorUtilities> adds named colors that `UnityEngine.Color` doesn't have: `orange`, `teal`, `purple`, `pink`, `pineGreen`, `transparent`, `emerald`, `amethyst`, `silver`, `gold`, `bronze` and `darkGray` (also `darkGrey`).

```csharp
var c = ColorUtilities.Brighten(baseColor, 0.2f);      // multiplies RGB by 1.2, clamped. Range -1 to 1.
var d = ColorUtilities.Darken(baseColor, 0.2f);        // the opposite of Brighten
var g = ColorUtilities.Grayscale(baseColor);           // luminosity method, keeps alpha

// Replace one channel and keep the rest.
var faded = ColorUtilities.GetColorWithAlpha(sprite.color, 0.5f);
var noRed = ColorUtilities.GetColorWithRed(sprite.color, 0f);
```

`GetColorWithGreen` and `GetColorWithBlue` work the same way.
