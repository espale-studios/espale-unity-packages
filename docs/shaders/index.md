# Shaders

`com.espale.shaders` is a set of Shader Graph shaders and sub graphs for 2D sprites in URP. The package has no C# code: you use the shaders through materials, and the sub graphs inside your own Shader Graphs.

## Install

```console
openupm add com.espale.shaders
```

**Dependencies:** Shader Graph `14.0.0`. See [Getting started](../getting-started.md) for other ways to install.

## Requirements

The shaders use URP's **Sprite Custom Lit** target, which needs the **2D Renderer**. Make sure your URP asset uses a Renderer 2D Data asset (**Create > Rendering > URP 2D Renderer**).

## Shaders

Pick these in a material's **Shader** dropdown, under **Espale**:

| Shader | What it does |
| --- | --- |
| [Espale/Sprite Boosted Lit](sprite-boosted-lit.md) | A lit sprite that reacts more strongly to 2D lights, with a cap on the brightness. |
| [Espale/Sprte Glitch](sprite-glitch.md) | An animated glitch: the sprite jitters sideways and its colors shift. |
| [Espale/Sprite Glitch (Pixelated)](sprite-glitch.md#pixelated-version) | The same glitch, snapped to the sprite's pixel grid for pixel art. |

> [!NOTE]
> The plain glitch shader's name is spelled **Sprte Glitch** in the menu. Search for "Glitch" if you can't find it.

## Sub graphs

Use these as nodes in your own Shader Graphs. Search for them by name in the **Create Node** menu, where they're grouped under **Espale**:

| Sub graph | What it does |
| --- | --- |
| [Glitch](sub-graphs/glitch.md) | Samples a texture with the animated glitch offset and color shift. |
| [Pixelate](sub-graphs/pixelate.md) | Snaps any UV to a texture's pixel grid. |
| [Pixelated UV](sub-graphs/pixelated-uv.md) | The mesh UV, snapped to a texture's pixel grid. |
| [Pixelated Screen Pos](sub-graphs/pixelated-screen-pos.md) | The screen position, snapped to a texture's pixel grid. |

## Using a shader on a sprite

1. Create a material: **Assets > Create > Material**.
2. Set its shader, for example **Espale > Sprite Boosted Lit**.
3. Assign the material to your `SpriteRenderer`'s **Material** field.

The sprite's texture is passed in automatically through `_MainTex`, so you don't need to set the texture on the material.
