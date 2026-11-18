# Sprite Boosted Lit

**Shader:** `Espale/Sprite Boosted Lit`

A lit sprite shader that strengthens the effect of 2D lights. The light reaching the sprite is multiplied by **Brightness Boost** and then capped at **Brightness Clamp**, so lit areas can glow brighter than with the default sprite shader without blowing out to white.

Use it for scenes where lights should feel punchy, such as torches in a dark cave or neon signs.

## Properties

| Property | Reference | Default | Range | Description |
| --- | --- | --- | --- | --- |
| Brightness Boost | `_Brightness_Boost` | `1` | 0 to 10 | How much the 2D light is amplified. |
| Brightness Clamp | `_Brightness_Clamp` | `1` | 0 to 10 | The highest light value allowed after boosting. Raise it to let lights get brighter. |
| MainTex | `_MainTex` | | | The sprite texture. The `SpriteRenderer` sets it for you. |

Set them from code with the reference names:

```csharp
spriteRenderer.material.SetFloat("_Brightness_Boost", 2.5f);
```

## Tips

- Start with both values at `1`, then raise **Brightness Boost** until lights look right, and raise **Brightness Clamp** only if bright spots look flat.
- The shader reads the light from the 2D Renderer's light texture, so it only reacts to **Light 2D** components.
- The graph doesn't read the sprite's vertex color, so the `SpriteRenderer`'s **Color** tint isn't applied.
