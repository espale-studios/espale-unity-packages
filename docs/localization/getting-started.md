# Setup and first key

## The localization folder

The first time anything reads localization data in the editor (opening one of the editor windows, or entering play mode with a `TextLocalizer` in the scene), the package creates this folder in your project:

```
Assets/
└── Localization/
    ├── csv_generator.py          Python helper, editor only
    ├── lng_file_generator.py     Python helper, editor only
    └── Resources/
        ├── _keys.txt             one key per line
        ├── _languages.txt        list of language files, used by builds
        └── en.txt                English values, one per line
```

It starts with English and the two built-in keys, `lng_name` and `lng_error`. Commit the whole `Assets/Localization` folder to version control.

> [!IMPORTANT]
> The language files must stay in `Assets/Localization/Resources/`. Builds load them with `Resources.Load`, and the editor reads them from that exact path.

## Built-in keys

| Key | Value |
| --- | --- |
| `lng_name` | The language's own name, such as `English` or `Türkçe`. Use it in language pickers. |
| `lng_error` | The text shown when a key has no value, such as `Localization Error`. |

Every language needs both. They can't be removed.

## Add a language

1. Open **Espale Studios > Localization > Language Manager**.
2. Pick the language, type its name (for `lng_name`) and its error message (for `lng_error`).
3. Select **Add "<Language>" language** and confirm. A new `<iso-code>.txt` file is created, for example `tr.txt` for Turkish.

## Add your first key

1. Open **Espale Studios > Localization > Key Editor**.
2. Type a key name, for example `menu_play`, and select **Add New Key**.
3. Fill in the value for each language and select **Update Values**.

Key names are stored in lower case and spaces become underscores, so `Menu Play` becomes `menu_play`.

## Show it on screen

Add a **Text Localizer** component to a GameObject with a TextMeshPro UI text or a legacy `Text`, and pick `menu_play` in its **Key** field. See [Text Localizer component](text-localizer.md).

## Pick the language at startup

The current language is English when the game starts and isn't saved between sessions. Set it yourself when the game boots, for example from a saved setting or from the player's system language:

```csharp
using Espale.Localization;
using UnityEngine;

public class LanguageBootstrap : MonoBehaviour
{
    private void Awake()
    {
        var language = LanguageTools.LanguageFromSystemLanguage(Application.systemLanguage);

        // SetCurrentLanguage returns false if the game has no file for that language.
        if (!Localizator.SetCurrentLanguage(language))
            Localizator.SetCurrentLanguage(Language.English);
    }
}
```

See [Scripting](scripting.md) for the rest of the API.
