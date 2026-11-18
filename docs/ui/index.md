# UI

`com.espale.ui` is a set of uGUI tools for game menus: a layer system that decides which menu has focus, ready-made widgets, automatic scrolling for gamepad and keyboard navigation, UI sounds, and debug overlays for FPS and the game version.

## Install

```console
openupm add com.espale.ui
```

**Dependencies:** [Utilities](../utilities/index.md), [Localization](../localization/index.md), TextMeshPro `3.0.4` and [NaughtyAttributes](https://github.com/dbrizov/NaughtyAttributes) `2.1.4`. See [Getting started](../getting-started.md) for other ways to install.

## Creating UI elements

Every element is a prefab you can create from either menu:

- **Espale Studios > UI** in the menu bar
- **GameObject > Espale UI**, also available by right-clicking in the Hierarchy

If the selected object isn't inside a `Canvas`, a new Screen Space Overlay canvas is created for the element.

| Menu item | Creates |
| --- | --- |
| **Layers > Blank Layer** | A canvas with a [`UILayer`](ui-layers.md) |
| **Widgets > Button** | A button with [UI sounds](ui-sounds/index.md) |
| **Widgets > Toggle** | An animated [Toggle](widgets/toggle.md) |
| **Widgets > Multi Select Dropdown** | A [Multi Select Dropdown](widgets/multi-select-dropdown.md) |
| **Widgets > Multi Select Dropdown with Icon** | A Multi Select Dropdown with an icon on each option |
| **Widgets > Input Visualizer** | A gamepad [button prompt](widgets/input-visualizer.md) |
| **Widgets > Linear Progress Bar** | A horizontal [Progress Bar](widgets/progress-bar.md) |
| **Widgets > Radial Progress Bar** | A circular [Progress Bar](widgets/progress-bar.md) |
| **Widgets > FPS Displayer** | An [FPS counter](debug-displays/fps-displayer.md) |
| **Widgets > Version Displayer** | A [version label](debug-displays/version-displayer.md) |

## What's inside

| Type | Namespace | Page |
| --- | --- | --- |
| <xref:Espale.UI.Layers.UILayer>, <xref:Espale.UI.Layers.UILayerManager> | `Espale.UI.Layers` | [UI layers](ui-layers.md) |
| <xref:Espale.UI.Toggle>, <xref:Espale.UI.ProgressBar>, <xref:Espale.UI.MultiSelectDropdown>, <xref:Espale.UI.InputVisualizer>, <xref:Espale.UI.InputVisualizerStyle> | `Espale.UI` | [Widgets](widgets/index.md) |
| <xref:Espale.UI.ScrollViewAutoScroller>, <xref:Espale.UI.DropdownAutoScroller> | `Espale.UI` | [Auto scrolling](auto-scrolling/index.md) |
| <xref:Espale.UI.SelectableAudioSource>, <xref:Espale.UI.UIAudioManager> | `Espale.UI` | [UI sounds](ui-sounds/index.md) |
| <xref:Espale.UI.FPSDisplayer>, <xref:Espale.UI.VersionDisplayer> | `Espale.UI` | [Debug displays](debug-displays/index.md) |

The package also includes click and whoosh sound effects in `Runtime/UI SFXs`, TextMeshPro Arial font assets, and simple shape sprites used by the prefabs.
