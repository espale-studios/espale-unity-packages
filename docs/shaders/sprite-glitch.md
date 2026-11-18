# Sprite Glitch

**Shaders:** `Espale/Sprte Glitch` and `Espale/Sprite Glitch (Pixelated)`

An animated glitch effect for sprites. Noise that changes over time pushes the sprite sideways in bands and shifts its hue, which suits holograms, corrupted enemies, damage flashes or a broken screen.

Both shaders use the [Glitch sub graph](sub-graphs/glitch.md) and have the same properties.

## Properties

| Property | Reference | Default | Range | Description |
| --- | --- | --- | --- | --- |
| Glitch Speed | `_Glitch_Speed` | `2` | 0 to 10 | How fast the glitch pattern changes. |
| Glitch Amount | `_Glitch_Amount` | `2` | | Strength of the glitch noise. |
| Color Change Amount | `_Color_Change_Amount` | `0.7` | 0 to 1 | How strongly the colors shift. |
| Color Change Speed | `_Color_Change_Speed` | `0.1` | 0 to 1 | How fast the colors shift. |
| MainTex | `_MainTex` | | | The sprite texture. The `SpriteRenderer` sets it for you. |

Turn the effect up during a hit and back down afterwards:

```csharp
var material = spriteRenderer.material;
material.SetFloat("_Glitch_Amount", 4f);
// ...later
material.SetFloat("_Glitch_Amount", 0f);
```

## Pixelated version

`Espale/Sprite Glitch (Pixelated)` first snaps the sprite's UV to its texture's pixel grid with the [Pixelated UV sub graph](sub-graphs/pixelated-uv.md). Use it for pixel art, so the glitch moves in whole pixels instead of smearing them.

## Tips

- Use `spriteRenderer.material` (an instance) rather than `sharedMaterial` when only one sprite should glitch.
- The glitch is driven by time, so every sprite with the same material glitches in sync.
- The graphs don't read the sprite's vertex color, so the `SpriteRenderer`'s **Color** tint isn't applied.
