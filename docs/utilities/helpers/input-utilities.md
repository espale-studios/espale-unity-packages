# InputUtilities

<xref:Espale.Utilities.InputUtilities> uses the new Input System to check what kind of gamepad the player has.

```csharp
if (InputUtilities.IsUsingGamepad())
{
    Gamepad pad = InputUtilities.GetCurrentGamepad();   // the first connected gamepad
    GamepadPlatform platform = InputUtilities.GetGamepadPlatform();
}
```

`GetGamepadPlatform()` returns <xref:Espale.Utilities.GamepadPlatform>:

- `Playstation` for DualShock and DualSense gamepads,
- `Nintendo` for the Switch Pro Controller,
- `Xbox` for any other gamepad,
- `None` when no gamepad is connected.

> [!NOTE]
> The Input System doesn't support the Switch Pro Controller on Linux, so on Linux builds it is reported as `Xbox`.

The <xref:Espale.Utilities.GamepadKey> enum names the buttons, sticks, triggers and D-pad directions of a generic gamepad. The UI package's [Input Visualizer](../../ui/widgets/input-visualizer.md) uses it to show button prompts.
