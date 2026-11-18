# Localization

`com.espale.localization` is a key-based text localization system. You give each piece of text a key such as `menu_play`, write a value for it in every language, and look it up at runtime with one call or with a component on your text object.

- Editor windows to browse, search, add, rename and remove keys and languages.
- A `TextLocalizer` component that updates `Text` and TextMeshPro UI labels whenever the language changes.
- A `[LocalizedKey]` attribute that turns any `string` field into a key picker with suggestions.
- Plain text files that work well with version control, plus Python scripts to export everything to a CSV for translators and import it back.
- Every language in ISO 639-1, plus Simplified Chinese.

## Install

```console
openupm add com.espale.localization
```

**Dependencies:** [Utilities](../utilities/index.md) and TextMeshPro `3.0.4`. See [Getting started](../getting-started.md) for other ways to install.

## Quick example

```csharp
using Espale.Localization;

// Read a value in the current language (falls back to English)
titleLabel.text = Localizator.GetString("menu_title");

// Switch language. Every TextLocalizer updates itself.
Localizator.SetCurrentLanguage(Language.Turkish);
```

## Next steps

1. [Setup and first key](getting-started.md): where the files live and how to add your first text.
2. [Editor tools](editor-tools/index.md): the Key Browser, Key Editor and Language Manager windows.
3. [Text Localizer component](text-localizer.md): localize UI text without code.
4. [Scripting](scripting.md): the `Localizator` API, language switching and the system language.
5. [Translating with CSV](csv-workflow.md): hand a spreadsheet to translators.
