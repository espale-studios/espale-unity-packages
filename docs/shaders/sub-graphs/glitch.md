# Glitch

**Category:** Espale

Samples a texture with an animated glitch: time-based noise shifts the UV and the hue. The [Sprite Glitch](../sprite-glitch.md) shaders are built on it.

| Input | Type | Default | Description |
| --- | --- | --- | --- |
| Texture | Texture 2D | | Texture to sample. |
| Texture UV | Vector 4 | | UV to sample at. Connect a UV node, or [Pixelated UV](pixelated-uv.md) for pixel art. |
| Glitch Speed | Float | `2` | How fast the glitch pattern changes. |
| Glitch Amount | Float | `2` | Strength of the glitch noise. |
| Color Change Amount | Float | `0.7` | How strongly the colors shift. |
| Color Change Speed | Float | `0.1` | How fast the colors shift. |

| Output | Type | Description |
| --- | --- | --- |
| Out_Vector4 | Vector 4 | The glitched color. RGB is the color and A is the texture's alpha. |
