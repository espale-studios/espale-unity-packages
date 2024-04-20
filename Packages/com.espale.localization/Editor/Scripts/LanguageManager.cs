using UnityEditor;
using UnityEngine;
using Espale.Utilities;
using System;

namespace Espale.Localization.Backend
{
    public class LanguageManager : EditorWindow
    {
        [MenuItem("Espale Studios/Localization/Language Manager")]
        private static void OpenWindow()
        {
            var window = CreateInstance<LanguageManager>();
            window.titleContent = new GUIContent("Language Manager");
            window.Show();
        }

        private Vector2 scrollPos;

        private Language languageToAdd = Language.English;
        private string languageName = "";
        private string localizationErrorMessage = "";

        private void OnEnable() =>  minSize = new Vector2(376, 250);

        private void OnGUI()
        {
            var languages = Localizator.GetAvailableLanguages();
            var localizationPercentages = Localizator.GetLocalizationPercentages();

            EditorScriptingUtilities.BeginCenter();
            GUILayout.Label("Current Language(s)", EditorStyles.largeLabel);
            EditorScriptingUtilities.EndCenter();

            EditorScriptingUtilities.HorizontalLine(3);

            var languageButtonStyle = new GUIStyle(EditorStyles.miniButton)
            {
                fixedHeight = 30f,
                fixedWidth = position.width * .3f,
                fontSize = 12,
                padding = new RectOffset(10, 10, 5, 5),
                alignment = TextAnchor.MiddleCenter,
                richText = true
            };

            scrollPos = GUILayout.BeginScrollView(scrollPos, false, true);
            foreach (var language in languages)
            {
                EditorScriptingUtilities.BeginCenter();
                GUILayout.BeginHorizontal();
                var localizationPercentage = localizationPercentages[language];
                
                // Label
                GUILayout.Label(
                    $"<color={LocPercentageToColor(localizationPercentage)}><b>{language}</b>, {localizationPercentage}% Localized</color>",
                    languageButtonStyle
                );
                
                // Switch to the language
                if (GUILayout.Button($"Switch", languageButtonStyle))
                {
                    Localizator.SetCurrentLanguage(language);
                    Debug.Log($"Switched to the {language} language.");
                }
                
                // Remove the language
                if (GUILayout.Button($"Remove", languageButtonStyle))
                {
                    var dialogOutput = EditorUtility.DisplayDialog(
                        $"{language} language will be removed from the project permanently",
                        $"Are you sure you want to remove {language} language from your project?",
                        "Yes",
                        "No");

                    if (dialogOutput)
                    {
                        Localizator.RemoveLanguage(language);
                        ((KeyBrowser) GetWindow(typeof(KeyBrowser))).UpdateArrays();
                    }

                }

                GUILayout.EndHorizontal();
                EditorScriptingUtilities.EndCenter();
            }

            GUILayout.EndScrollView();

            EditorScriptingUtilities.HorizontalLine(3);

            // Language
            GUILayout.BeginHorizontal();
            GUILayout.Label("Language to add: ");
            
            languageToAdd = (Language) EditorGUILayout.EnumPopup(languageToAdd, GUILayout.Width(200f));
            GUILayout.EndHorizontal();

            // Language Name
            GUILayout.BeginHorizontal();
            GUILayout.Label("Language Name: ");
            languageName = GUILayout.TextField(languageName, GUILayout.Width(200f));
            GUILayout.EndHorizontal();

            // Localization Error Message
            GUILayout.BeginHorizontal();
            GUILayout.Label("Localization Error Message: ");
            localizationErrorMessage = GUILayout.TextField(localizationErrorMessage, GUILayout.Width(200f));
            GUILayout.EndHorizontal();

            var canCreateLanguage = Array.IndexOf(Localizator.GetAvailableLanguages(), languageToAdd) == -1;

            if (canCreateLanguage)
            {
                if (GUILayout.Button($"Add \"{languageToAdd}\" language"))
                {
                    var dialogOutput = EditorUtility.DisplayDialog(
                        $"{languageToAdd} language file will be created",
                        "Are you sure you want to add this language ?",
                        "Add",
                        "Cancel"
                    );

                    if (!dialogOutput) return;
                    Localizator.AddLanguage(languageToAdd);
                        
                    if (!string.IsNullOrWhiteSpace(languageName)) 
                        Localizator.AddLocalizedValue(Localizator.LNG_NAME_KEY, languageName, languageToAdd);
                        
                    if (!string.IsNullOrWhiteSpace(localizationErrorMessage)) 
                        Localizator.AddLocalizedValue(Localizator.LOCALIZATION_ERROR_KEY, localizationErrorMessage, languageToAdd);
                        
                    ((KeyBrowser) GetWindow(typeof(KeyBrowser))).UpdateArrays();
                }
                
            }
            else
            {
                EditorGUILayout.HelpBox("Cannot add the selected language",
                    MessageType.Warning);
            }
        }

        private static string LocPercentageToColor(float percentage)
        {
            percentage = Mathf.Clamp(percentage, 0f, 100f);

            Color color;
            if (percentage >= 50f)
                color = Color.Lerp(Color.yellow, Color.green, (percentage - 50f) / 50f);
            else
                color = Color.Lerp(Color.red, Color.yellow, percentage / 50f);

            return $"#{ColorUtility.ToHtmlStringRGB(color)}";
        }
    }
}
