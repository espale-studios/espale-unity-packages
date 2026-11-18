# Localizator

Everything goes through the static <xref:Espale.Localization.Localizator> class in the `Espale.Localization` namespace.

```csharp
using Espale.Localization;
```

## Reading values

```csharp
// Current language, then the fallback language (English by default), then lng_error.
string text = Localizator.GetString("menu_play");

// Custom fallback, and an empty string instead of the error text.
string text2 = Localizator.GetString("menu_play", Language.German, returnErrorString: false);

// Only the current language, no fallback.
string text3 = Localizator.GetStringWithCurrentLanguage("menu_play");

// A specific language, regardless of the current one.
string turkish = Localizator.GetStringWithLanguage("menu_play", Language.Turkish);
```

The `<line_break>` markers in the files become real line breaks in the returned text, and surrounding whitespace is trimmed.

Values work well as format strings:

```csharp
// "score_label" = "Score: {0}"
scoreText.text = string.Format(Localizator.GetString("score_label"), score);
```

## Languages

```csharp
Language current = Localizator.GetCurrentLanguage();
Language[] available = Localizator.GetAvailableLanguages();   // languages that have a file
int count = Localizator.GetLanguageCount();

// Returns false, and changes nothing, if the language has no file.
bool changed = Localizator.SetCurrentLanguage(Language.Spanish);
```

### Reacting to language changes

```csharp
private void OnEnable() => Localizator.OnLanguageChange += Refresh;
private void OnDisable() => Localizator.OnLanguageChange -= Refresh;

private void Refresh() => title.text = Localizator.GetString("menu_title");
```

`OnLanguageChange` fires after `SetCurrentLanguage` succeeds, and in play mode also after any key or value is changed.

### Building a language picker

Each language's `lng_name` is its own name, so a picker can list languages in their own words:

```csharp
foreach (var language in Localizator.GetAvailableLanguages())
{
    string name = Localizator.GetStringWithLanguage(Localizator.LNG_NAME_KEY, language);
    // add an option showing `name` that calls Localizator.SetCurrentLanguage(language)
}
```

### Language codes and the system language

<xref:Espale.Localization.LanguageTools> converts between the <xref:Espale.Localization.Language> enum, ISO 639-1 codes and Unity's `SystemLanguage`:

```csharp
string code = LanguageTools.IsoCodeFromLanguage(Language.Turkish);    // "tr"
Language lang = LanguageTools.LanguageFromIsoCode("de");              // Language.German
Language system = LanguageTools.LanguageFromSystemLanguage(Application.systemLanguage);
```

The `Language` enum has every ISO 639-1 language, plus `ChineseSimplified` (file code `zh_simplified`) and `Unknown`.

## Keys and values from code

These methods change the localization files. They save to disk only in the editor, so use them for editor tooling, not at runtime in a build.

| Method | Description |
| --- | --- |
| `AddKey(key)` | Adds an empty key to every language. Returns `false` if it already exists. |
| `RemoveKey(key)` | Removes a key from every language. The built-in keys can't be removed. |
| `RenameKey(key, newName)` | Renames a key. |
| `AddLocalizedValue(key, value, language)` | Sets one value. |
| `AddLocalizedValues(key, values, languages)` | Sets several values for one key. The two arrays must be the same length. |
| `AddLanguage(language)`, `RemoveLanguage(language)` | Adds a language (and creates its file) or removes it. `RemoveLanguage` doesn't delete the file. |
| `DoesContainKey(key)` | Whether a key exists. |
| `ConvertRawToKey(text)` | Converts text to key format: lower case, spaces become underscores, line breaks removed. |
| `RefreshAll(forceReadLocFile)` | Refreshes the asset database, and re-reads the files if `forceReadLocFile` is `true`. |

## Statistics

```csharp
// Percentage of keys with a value, per language, rounded to 2 decimals.
Dictionary<Language, float> progress = Localizator.GetLocalizationPercentages();

// For one key: whether each available language has a value, in the order of GetAvailableLanguages().
bool[] status = Localizator.GetLocalizationStatusOfKey("menu_play");
```
