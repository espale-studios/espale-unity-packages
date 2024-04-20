using System;
using Espale.Utilities;
using UnityEditor;
using UnityEngine;

namespace Espale.Localization.Backend
{
    public class KeyEditor : EditorWindow
    {
        [MenuItem("Espale Studios/Localization/Key Editor")]
        private static void OpenWindow()
        {
            var window = CreateInstance<KeyEditor>();
            window.titleContent = new GUIContent("Key Editor");
            window.Show();
        }

        public static string key = "";
        public string keyLastValue;
        public string[] values;
        private Vector2 scrollPos = Vector2.zero;
        private string newName = "";

        private void OnEnable()
        {
            minSize = new Vector2(100f, 100f);
        }

        private void OnGUI()
        {
            var keySearchBarStyle = new GUIStyle(EditorStyles.textField)
            {
                fixedWidth = position.width * .99f,
                fixedHeight = 23f,
                alignment = TextAnchor.MiddleCenter,
                fontSize = 12
            };

            key = GUILayout.TextField(key, keySearchBarStyle);
            key = Localizator.ConvertRawToKey(key);

            GUILayout.Space(10f);

            if (key != keyLastValue || values.Length != Localizator.GetLanguageCount())
                values = new string[Localizator.GetLanguageCount()];

            if (Localizator.DoesContainKey(key))
            {
                scrollPos = GUILayout.BeginScrollView(scrollPos, false, true);
                for (var i = 0; i < values.Length; i++)
                {
                    var localizedVal = Localizator.GetStringWithLanguage(key, Localizator.GetAvailableLanguages()[i], false);
                    values[i] ??= localizedVal;

                    GUILayout.BeginHorizontal();
                    
                    GUI.color = values[i] == localizedVal ? Color.green : Color.red;
                    EditorGUILayout.LabelField($"{Localizator.GetAvailableLanguages()[i]}: ", GUILayout.MaxWidth(50));
                    GUI.color = Color.white;

                    var customTextAreaStyle = EditorStyles.textArea;
                    customTextAreaStyle.wordWrap = true;
                    customTextAreaStyle.fixedHeight = 0;
                    customTextAreaStyle.stretchHeight = true;

                    values[i] = EditorGUILayout.TextArea(values[i], customTextAreaStyle);

                    GUILayout.EndHorizontal();
                }

                GUILayout.EndScrollView();

                if (GUILayout.Button("Update Values"))
                {
                    Localizator.AddLocalizedValues(key, values, Localizator.GetAvailableLanguages());
                    values = Array.Empty<string>();
                    
                    RefreshKeyBrowser();
                }

                if (GUILayout.Button("Remove Key"))
                {
                    var dialogOutput = EditorUtility.DisplayDialog(
                        $"{key} will be removed permanently",
                        "Are you sure you want to remove this key?",
                        "Remove",
                        "Cancel"
                    );
                    if (dialogOutput)
                    {
                        Localizator.RemoveKey(key);
                        values = Array.Empty<string>();

                        RefreshKeyBrowser();
                    }
                }
                
                // Renaming
                EditorScriptingUtilities.BeginCenter();

                var canRename = !Localizator.DoesContainKey(newName.Trim()) && !string.IsNullOrWhiteSpace(newName.Trim());
                if (GUILayout.Button("Rename Key", GUILayout.Width(position.width * .5f)))
                {
                    if (canRename)
                    {
                        Localizator.RenameKey(key, newName.Trim());

                        key = newName.Trim();
                        newName = "";
                        RefreshKeyBrowser();
                    }
                }

                newName = Localizator.ConvertRawToKey(GUILayout.TextField(newName, GUILayout.Width(this.position.width * .5f)));
                EditorScriptingUtilities.EndCenter();
                if (Localizator.DoesContainKey(newName.Trim()))
                    EditorGUILayout.HelpBox("The key name entered is already in use.",
                        MessageType.Warning);
            }
            else if (GUILayout.Button("Add New Key") && !string.IsNullOrEmpty(key) && !string.IsNullOrWhiteSpace(key))
            {
                Localizator.AddKey(key);
                RefreshKeyBrowser();
            }

            keyLastValue = key;
        }

        private static void RefreshKeyBrowser()
        {
            var window = (KeyBrowser) GetWindow( typeof(KeyBrowser), false,"Key Browser",false);
            window.Refresh();
            window.SetLocalizationStatus(LocalizationStatus.All);
        }
    }
}
