# InputVisualizer

<xref:Espale.UI.InputVisualizer> shows the right button prompt for the player's gamepad: an `Image` that displays, for example, the "A" button on Xbox and the "×" button on PlayStation for the same action.

| Field | Description |
| --- | --- |
| **Auto Pick Gamepad** | Every frame, switch to the style of the connected gamepad using `InputUtilities.GetGamepadPlatform()`. |
| **Current Gamepad Platform** | The platform to show when **Auto Pick Gamepad** is off. |
| **Playstation Style**, **Xbox Style**, **Nintendo Style** | The sprite set to use for each platform. |
| **Keyboard Sprite** | Shown when no gamepad is connected (platform `None`). |
| **Key** | Which button to show, as a <xref:Espale.Utilities.GamepadKey>. |

```csharp
// Switch the platform yourself, for example from your own input device tracking
inputVisualizer.UpdateGamepad(GamepadPlatform.Playstation);
```

## Styles

An <xref:Espale.UI.InputVisualizerStyle> is an asset with one sprite per button, stick, trigger and D-pad direction. The package includes these styles in `Runtime/Input Visualizer Styles`:

- **PlayStation:** PS Flat, PS Flat Colored, PS Pixelart Colored
- **Xbox:** Xbox Flat, Xbox Flat Colored, Xbox Pixelart Colored

Make your own with **Assets > Create > Espale Studios > UI > Input Visualizer Style** and assign a sprite to each slot. There's no Nintendo style included, so create one if you support Switch controllers.
