# Getting started

## Requirements

- Unity **2022.3** or newer
- The **Universal Render Pipeline** (URP)
- The **Input System** package (used by `com.espale.utilities`)

## Install with the OpenUPM CLI

Install the [OpenUPM CLI](https://openupm.com/docs/getting-started-cli.html), open a terminal in your Unity project's root folder and add the packages you need:

```console
openupm add com.espale.utilities
openupm add com.espale.localization
openupm add com.espale.ui
openupm add com.espale.shaders
```

The CLI adds the OpenUPM scoped registry to your project and installs each package's dependencies.

## Install through the Package Manager window

1. Open **Edit > Project Settings > Package Manager**.
2. Add a new scoped registry:
   - **Name:** `package.openupm.com`
   - **URL:** `https://package.openupm.com`
   - **Scopes:** `com.espale`, `com.dbrizov.naughtyattributes`
3. Open **Window > Package Manager**, switch the registry dropdown to **My Registries** and install the packages you want.

Or add the same thing to `Packages/manifest.json` directly:

```json
{
  "scopedRegistries": [
    {
      "name": "package.openupm.com",
      "url": "https://package.openupm.com",
      "scopes": [
        "com.espale",
        "com.dbrizov.naughtyattributes"
      ]
    }
  ],
  "dependencies": {
    "com.espale.utilities": "1.1.0"
  }
}
```

Check each package's page on [OpenUPM](https://openupm.com/packages/?q=com.espale) for its latest version.

## Assemblies and namespaces

Each package ships its code in its own assembly definitions, so if your scripts use an assembly definition of their own, add a reference to the Espale assembly you use.

| Package | Runtime assembly | Editor assembly | Namespaces |
| --- | --- | --- | --- |
| Utilities | `Espale.Utilities.Runtime` | `Espale.Utilities.Editor` | `Espale.Utilities`, `Espale.Utilities.Components`, `Espale.Utilities.Components.Colliders`, `Espale.Utilities.Components.Randomizers` |
| Localization | `Espale.Localization.Runtime` | `Espale.Localization.Editor` | `Espale.Localization` |
| UI | `Espale.UI.Runtime` | `Espale.UI.Editor` | `Espale.UI`, `Espale.UI.Layers` |
| Shaders | (no code) | | |

## Editor menus

All packages add their tools under the **Espale Studios** menu in the Unity menu bar. The UI package also adds its prefabs under **GameObject > Espale UI**, so you can create them by right-clicking in the Hierarchy.
