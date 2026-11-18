# File format

The localization data is stored as plain UTF-8 text files in `Assets/Localization/Resources/`. You rarely need to edit them by hand, but knowing the format helps with merges and reviews.

## `_keys.txt`

One key per line. The line number of a key is its index in every language file.

```text
lng_name
lng_error
menu_play
menu_quit
```

## `<iso-code>.txt`

One file per language, named after its ISO 639-1 code (`en.txt`, `tr.txt`, `de.txt`, ...). Simplified Chinese uses `zh_simplified.txt`. Line *N* is the value of the key on line *N* of `_keys.txt`. An empty line means the key has no value in that language.

```text
English
Localization Error
Play
Quit
```

Line breaks inside a value are stored as `<line_break>`:

```text
Press any key<line_break>to continue
```

## `_languages.txt`

The list of language files, one per line (`en.txt`, `tr.txt`, ...). Builds can't list the files in a `Resources` folder, so they read this list to know which languages exist. The editor rewrites it every time it reads or saves the localization files.

## Merging

Because values are matched to keys by line number, a merge must keep `_keys.txt` and every language file the same length and in the same order. When two people add keys at the same time:

1. Resolve `_keys.txt` first.
2. Resolve each language file so its lines match the new key order.
3. Open the [Key Browser](editor-tools/key-browser.md), select **Refresh** and check for missing values.

If a merge gets messy, exporting both sides to CSV and comparing the sheets is often easier.
