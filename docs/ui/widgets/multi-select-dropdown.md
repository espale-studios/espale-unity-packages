# MultiSelectDropdown

<xref:Espale.UI.MultiSelectDropdown> works like a TextMeshPro dropdown, but the player can tick several options. It's useful for filters and tag pickers. **Multi Select Dropdown with Icon** shows an icon next to each option.

## Options

Each entry in **Options** has a **Text**, an optional **Icon**, a **Bg Color** and a **Text Color**.

```csharp
using Espale.UI;
using System.Collections.Generic;

dropdown.SetOptions(new List<MultiSelectDropdown.MultiSelectDropdownOptionData>
{
    new() { text = "Swords" },
    new() { text = "Bows" },
    new() { text = "Staves" },
});

dropdown.onSelectionChanged.AddListener(selected =>
{
    // `selected` is the sorted list of selected option indexes
});

dropdown.SetSelections(new List<int> { 0, 2 });
List<int> indexes = dropdown.GetSelectedIndexes();
List<string> labels = dropdown.GetSelectedOptionLabels();
dropdown.ClearSelections();
```

## Label text

The closed dropdown's label sums up the selection:

| Field | Default | Used when |
| --- | --- | --- |
| **No Selection Text** | `Select...` | Nothing is selected. |
| **Selection Merge Text** | `{0}, {1}` | Joining selected labels. `{0}` is the text so far, `{1}` the next label. |
| **Contains More Text** | `+{0} more` | More options are selected than **Max Selection Count To Display More Text**. `{0}` is the number left. |
| **Max Selection Count To Display More Text** | `2` | How many labels to show before switching to the "more" text. |

Each text also has a **Key** field. When a key is set, the text comes from [Localization](../../localization/index.md) instead.

With the defaults, selecting four options shows `Swords, Bows, +2 more`.

## Other fields

| Field | Description |
| --- | --- |
| **Options Sort Type** | Order of options in the open list: `ConstantOrder`, `SelectionsFirst` or `SelectionsLast`. |
| **Label Icon**, **Multiple Choices Label Icon** | The icon on the closed dropdown. With one selection it shows that option's icon, with several it shows **Multiple Choices Label Icon** if set. |
| **Dropdown Template**, **Item Text**, **Item Icon**, **Item Bg** | The template used to build the open list. Its item must have an `Espale.UI.Toggle`. The prefabs set these up. |

The list closes when the player clicks outside it or presses Cancel.
