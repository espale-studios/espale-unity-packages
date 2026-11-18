# Translating with CSV

Translators usually prefer a spreadsheet to separate text files. The package ships two Python 3 scripts that convert the language files to a single CSV and back. They're copied to `Assets/Localization/` along with the language files, and use only Python's standard library.

> [!IMPORTANT]
> Both scripts work on the files in the **current folder**, so run them from inside `Assets/Localization/Resources/`.

## Export to CSV

```console
cd Assets/Localization/Resources
python ../csv_generator.py
```

This creates `espale_localization_sheet.csv`. The first column, `Keys`, holds the keys. Each other column holds one language and is named after its file, for example `en` or `tr`. Line breaks inside a value are written as real line breaks in the cell.

Open it in any spreadsheet app (Google Sheets, Excel, LibreOffice), share it with translators and fill in the empty cells.

## Import from CSV

Export the sheet back to CSV with UTF-8 encoding, put it in `Assets/Localization/Resources/` and run:

```console
cd Assets/Localization/Resources
python ../lng_file_generator.py
```

The script asks for the CSV's name without `.csv`. Press Enter to use `espale_localization_sheet`. It then rewrites `_keys.txt` and one `<column name>.txt` file per language column.

After importing:

1. Delete or move the `.csv` file out of `Resources`, so it isn't included in your build.
2. Select **Refresh** in the [Key Browser](editor-tools/key-browser.md).

## Things to know

- The import **sorts keys alphabetically**. This changes the line order of every file, which shows up as a large diff in version control but doesn't change any values.
- Column headers must be the language file names (`en`, `tr`, `zh_simplified`, ...). Don't rename them.
- Add new languages with the [Language Manager](editor-tools/language-manager.md) before you export. A language column added only in the spreadsheet is written as a file, but `_languages.txt` isn't updated until the editor reads the files again.
- Every row needs a value cell for every language, even if it's empty.
