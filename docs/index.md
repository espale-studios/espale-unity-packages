---
title: Espale Unity Packages
_layout: landing
---

# Espale Unity Packages

Open-source Unity packages made by **Espale Studios** while building our own games. They are free to use under the MIT License and installable through [OpenUPM](https://openupm.com/).

> [!NOTE]
> The packages target **Unity 2022.3 or newer**, the **Universal Render Pipeline (URP)** and the **new Input System**. They are designed with 2D games in mind, though most of the code works in 3D too.

<div class="espale-cards">
  <a class="espale-card" href="utilities/index.md">
    <strong>Utilities</strong>
    Math, random, color, save and debug helpers, a generic singleton and small 2D components.
    <br><code>com.espale.utilities</code>
  </a>
  <a class="espale-card" href="localization/index.md">
    <strong>Localization</strong>
    Key-based text localization with editor tools and a CSV round trip for translators.
    <br><code>com.espale.localization</code>
  </a>
  <a class="espale-card" href="ui/index.md">
    <strong>UI</strong>
    A layered UI focus system, widgets, auto scrolling, UI sounds and debug overlays.
    <br><code>com.espale.ui</code>
  </a>
  <a class="espale-card" href="shaders/index.md">
    <strong>Shaders</strong>
    Shader Graph shaders and sub graphs for 2D sprites: boosted lighting, glitch and pixelation.
    <br><code>com.espale.shaders</code>
  </a>
</div>

## Quick install

With the [OpenUPM CLI](https://openupm.com/docs/getting-started-cli.html) installed, run the commands for the packages you need in your Unity project's folder:

```console
openupm add com.espale.utilities
openupm add com.espale.localization
openupm add com.espale.ui
openupm add com.espale.shaders
```

Dependencies, including other Espale packages, are installed for you. See [Getting started](getting-started.md) for installing without the CLI.

## How the packages fit together

| Package | Depends on |
| --- | --- |
| [Utilities](utilities/index.md) | Input System, URP, [NaughtyAttributes](https://github.com/dbrizov/NaughtyAttributes) |
| [Localization](localization/index.md) | Utilities, TextMeshPro |
| [UI](ui/index.md) | Utilities, Localization, TextMeshPro, NaughtyAttributes |
| [Shaders](shaders/index.md) | Shader Graph |

OpenUPM installs these dependencies for you.

## Links

- [Source code on GitHub](https://github.com/espale-studios/espale-unity-packages)
- [Report a bug or request a feature](https://github.com/espale-studios/espale-unity-packages/issues)
- [Espale Studios](https://espalestudios.com)
