# UILayer and UILayerManager

Game menus stack: a pause menu opens over the HUD, a settings page opens over the pause menu, a confirmation popup opens over the settings. The UI layer system keeps track of that stack and decides which menu is visible, which one has focus and which ones can be clicked.

Each menu is a <xref:Espale.UI.Layers.UILayer>, and the static <xref:Espale.UI.Layers.UILayerManager> keeps the stack of open layers.

## Setting up a layer

1. Create **Espale Studios > UI > Layers > Blank Layer**. It's a canvas with a `CanvasGroup` and a `UILayer`.
2. Build your menu inside it.
3. Set the layer's fields:

| Field | Description |
| --- | --- |
| **Layer Order** | Where the layer sits in the stack. `1` is the bottom. A HUD could be `1`, the pause menu `2` and its popups `3`. It's also used as the canvas's sorting order. |
| **Default Active Layer** | Focus this layer when the scene starts. Turn it on for at most one layer per scene; the editor logs an error if more than one has it. |
| **Block Layers Below** | While this layer is open, the layers below it can't be clicked or selected. Turn it off for overlays that should let the player keep using the menu underneath. |
| **Default Selectable** | The button or other `Selectable` to select when the layer gets focus, so gamepad and keyboard navigation start in the right place. |

Layers that aren't the default active layer are hidden when the scene starts.

## Layer states

A layer is always in one of three <xref:Espale.UI.Layers.UILayer.UILayerState> states:

| State | Visible | Interactable |
| --- | --- | --- |
| `Inactive` | No | No |
| `Sleep` | Yes | Only if no layer above blocks it |
| `Focused` | Yes | Yes |

## Opening and closing layers

From a UI event such as a button's **On Click**, drag in the layer and call one of its methods:

| Method | Effect |
| --- | --- |
| `UILayer.Focus()` | Opens the layer and gives it focus. |
| `UILayer.Hide()` | Closes the layer and every layer above it. |
| `UILayer.HideIfFocused()` | Closes the layer only if it currently has focus. |

From code, use `UILayerManager`:

```csharp
using Espale.UI.Layers;

UILayerManager.FocusLayer(pauseMenu);   // open and focus
UILayerManager.ToggleLayer(pauseMenu);  // open if closed, close if open
UILayerManager.HideLayer(pauseMenu);    // close it and everything above it
UILayerManager.HideTopLayer();          // close whatever is on top, e.g. for a Back button
```

### What focusing does to other layers

When you focus a layer with order *N*:

- If the top of the stack is below *N*, the current top layer goes to sleep (stays visible) and the new layer opens above it.
- If a layer with order *N* is already open, it's closed and replaced by the new one.
- If layers above *N* are open, they're all closed first.

When the top layer is closed, the next layer down gets focus again.

```csharp
// A typical Back button handler for gamepads
void OnCancel() => UILayerManager.HideTopLayer();
```

## Custom open and close animations

By default a layer shows and hides by setting its `CanvasGroup` alpha to 1 or 0. Make a subclass and override `ChangeVisibility` to animate it instead. The `canvasGroup` and `canvas` fields are `protected` for this:

```csharp
using System.Collections;
using Espale.UI.Layers;
using UnityEngine;

public class FadingUILayer : UILayer
{
    [SerializeField] private float duration = 0.15f;
    private Coroutine fade;

    protected override void ChangeVisibility(bool visible)
    {
        if (fade != null) StopCoroutine(fade);
        if (!isActiveAndEnabled) { canvasGroup.alpha = visible ? 1f : 0f; return; }
        fade = StartCoroutine(Fade(visible ? 1f : 0f));
    }

    private IEnumerator Fade(float target)
    {
        var start = canvasGroup.alpha;
        for (var t = 0f; t < duration; t += Time.unscaledDeltaTime)
        {
            canvasGroup.alpha = Mathf.Lerp(start, target, t / duration);
            yield return null;
        }
        canvasGroup.alpha = target;
    }
}
```

> [!NOTE]
> `UILayer` uses `Awake` and `Update`. If your subclass needs them, hide them with `new` and call `base.Awake()` or `base.Update()` first.
