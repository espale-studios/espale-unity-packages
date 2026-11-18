# LanguageManager

**Espale Studios > Localization > Language Manager** manages the languages in your project.

- **Current Language(s)** lists each language with how much of it is localized, colored from red (0%) through yellow to green (100%).
- **Switch** makes that language the current one, so you can preview it in the editor.
- **Remove** removes the language from the project, after a confirmation. Its `<iso-code>.txt` file stays in `Resources`, so delete that file too, or the language is loaded again the next time the files are read.
- To add a language, pick it, fill in **Language Name** (saved as `lng_name`) and **Localization Error Message** (saved as `lng_error`), then select **Add "<Language>" language**.
