# TextLocalizer

<xref:Espale.Localization.TextLocalizer> sets the text of a UI label to a localized value and updates it whenever the language changes. Add it to a GameObject that has a **TextMeshPro - Text (UI)** (`TextMeshProUGUI`) or a legacy **Text** component.

| Field | Description |
| --- | --- |
| **Key** | The key to show. It has the [key picker](editor-tools/localized-key-attribute.md) with suggestions. |
| **Display Error Message** | When the key has no value, show the language's `lng_error` text. Turn it off to leave the text empty instead. |
| **Use Fallback Language** | When the current language has no value for the key, use the fallback language's value. |
| **Fallback Language** | The language to fall back to. English by default. |
| **Prefix**, **Suffix** | Text added before and after the localized value, such as `"> "` or `":"`. They aren't localized. |

The component runs in edit mode too, so the label shows the localized text in the Scene view.

## Updating from code

The component listens to `Localizator.OnLanguageChange` while it's enabled, so you don't need to refresh it after switching languages. If you change its **Key** from code, call `UpdateLanguage()`:

```csharp
localizer.key = "menu_continue";
localizer.UpdateLanguage();
```

> [!NOTE]
> `TextLocalizer` looks for `TextMeshProUGUI`, the UI version of TextMeshPro. For 3D world-space TextMeshPro text, set the text from code with `Localizator.GetString`.
