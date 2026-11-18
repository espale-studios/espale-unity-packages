# Light2DSpriteMatcher

<xref:Espale.Utilities.Components.Light2DSpriteMatcher> (`Espale.Utilities.Components`) copies a `SpriteRenderer`'s current sprite into a sprite-type URP `Light2D` every frame, in edit mode and play mode. Use it for lights that should follow an animated sprite, such as a flickering torch.

1. Add a **Light 2D** and set its type to **Sprite**.
2. Add **Light 2D Sprite Matcher** to the same GameObject.
3. Drag the `SpriteRenderer` to copy from into **Target**.

> [!NOTE]
> URP doesn't expose the light's sprite publicly, so this component sets it through reflection. If a future URP version renames that internal field, the component will stop working.
