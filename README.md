<div align="center">

<img src="https://raw.githubusercontent.com/espale-studios/.github/main/profile/img/logo.png" alt="Logo" height="210"></h1>

<i>"Art for art's sake"</i>

<img src="https://raw.githubusercontent.com/espale-studios/website/main/img/home_hero.webp" align="right"
     alt="Size Limit logo by Anton Lovchikov" width="212" height="160">
<br/>

We are a small passionate team of game developers based in Turkey who believe that games are the finest form of art. We believe in the philosophy of "art for art's sake" and our goal is to combine this philosophy with game development to create unforgettable digital experiences.

<a href="https://www.youtube.com/@espalestudios"><img src="https://raw.githubusercontent.com/espale-studios/.github/main/profile/img/yt.svg" width=30 height=30></img></a>
<a href="https://twitter.com/EspaleStudios"><img src="https://raw.githubusercontent.com/espale-studios/.github/main/profile/img/tw.svg" width=30 height=30></img></a>
<a href="https://www.instagram.com/espale.studios/"><img src="https://raw.githubusercontent.com/espale-studios/.github/main/profile/img/ig.svg" width=30 height=30></img></a>
<a href="https://www.tiktok.com/@espale.studios"><img src="https://raw.githubusercontent.com/espale-studios/.github/main/profile/img/ti.svg" width=30 height=30></img></a>
<a href="https://www.linkedin.com/company/espale-studios"><img src="https://raw.githubusercontent.com/espale-studios/.github/main/profile/img/li.svg" width=30 height=30></img></a>
<a href="https://store.steampowered.com/publisher/EspaleStudios/"><img src="https://raw.githubusercontent.com/espale-studios/.github/main/profile/img/st.svg" width=30 height=30></img></a>

</div>

# **Espale Unity Packages**

[![stargazers](https://img.shields.io/github/stars/espale-studios/spale-unity-packages?color=yellow)](https://github.com/espale-studios/espale-unity-packages/stargazers)
[![openupm](https://img.shields.io/npm/v/com.espale.localization?label=openupm%20(localization)&registry_uri=https://package.openupm.com)](https://openupm.com/packages/com.espale.localization/)
[![openupm](https://img.shields.io/npm/v/com.espale.shaders?label=openupm%20(shaders)&registry_uri=https://package.openupm.com)](https://openupm.com/packages/com.espale.shaders/)
[![openupm](https://img.shields.io/npm/v/com.espale.ui?label=openupm%20(UI)&registry_uri=https://package.openupm.com)](https://openupm.com/packages/com.espale.ui/)
[![openupm](https://img.shields.io/npm/v/com.espale.utilities?label=openupm%20(utilities)&registry_uri=https://package.openupm.com)](https://openupm.com/packages/com.espale.utilities/)

This is an open-source _monorepo_ of _Unity_ packages, developed by **Espale Studios**.

## About Espale Studios and our Packages

> ⚠️ Our packages are designed with our games in mind, meaning they are designed for 2D games using the new input system & URP.

[Here](https://github.com/espale-studios/espale-unity-packages/wiki) is the link to the documentation.

We are a small passionate team of game developers based in Turkey. We believe using a modular approach is the best way of development so we divide our code into packages when we believe they can be useful in other projects.

## Installation

[OpenUPM](https://github.com/openupm/openupm) is an open-source service for hosting and building open-source Unity Package Manager (UPM) packages. After setting up [OpenUPM Client](https://github.com/openupm/openupm-cli#openupm-cli) you can run the following command to install a package from the _OpenUPM Client_. Installation commands for each package can be found [below](#packages).

```console
openupm add com.espale.{{PACKAGE_NAME_HERE}}
```

## Packages

### 1. Localization (`com.espale.localization`)

Contains general tools for key-based text localization with a simple-to-use API.
- Simple and Fast Editor Tools for _key browsing_, _key editing_ and _managing languages_
- Easy export/import to _CSV_ files via _Python_ scripts.

#### Installation

```console
openupm add com.espale.localization
```

### 2. Shaders (`com.espale.shaders`)

Contains common _shaders_ and useful _sub-shaders_

#### Included Shaders
- Sprite Glitch
- Sprite Glitch (Pixelated)
- Sprite Boosted Lit

#### Included Sub-Shaders
- Glitch
- Pixelated Screen Pos
- Pixelated UV
- Pixelate

#### Installation

```console
openupm add com.espale.shaders
```

### 3. UI (`com.espale.ui`)

Contains useful UI components and tools.
- Improved versions of common components such as Buttons and Toggles with built-in SFX support.
- Useful UI Prefabs for Displaying the version and FPS.
- Progress Bars
- Input Prompt Visualizers

#### Installation

```console
openupm add com.espale.ui
```

### 4. Utilities (`com.espale.utilities`)

Contains useful Runtime & Editor classes to fasten your development process.

#### Installation

```console
openupm add com.espale.utilities
```

## Support

If you have any problems using our packages, you can open an issue on the corresponding repository.

> ✉️ If you believe the issue is urgent and requires immident contact, you can contact us via [support@espalestudios.com](mailto:support@espalestudios.com)

## List of Games Using Espale Packages

- [Ingression: Platforming with Portals](https://store.steampowered.com/app/1966970) by [Espale Studios](https://www.espalestudios.com)
- [Project Chemistry](https://store.steampowered.com/app/1270620) by [Ata Türkoğlu](https://github.com/AtaTrkgl) & [Canber Demir](https://www.linkedin.com/in/canberkdemir3/)

> ✉️ If you use any package from this _monorepo_ feel free to send us an email at  [contact@espalestudios.com](mailto:contact@espalestudios.com) for us to add your game to the list.
