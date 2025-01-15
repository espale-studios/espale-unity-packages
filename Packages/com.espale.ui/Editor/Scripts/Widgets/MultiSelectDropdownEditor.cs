using UnityEngine;
using UnityEditor;
using UnityEditor.UI;

namespace Espale.UI.Editor
{
    [CustomEditor(typeof(MultiSelectDropdown), true)]
    [CanEditMultipleObjects]
    public class MultiSelectDropdownEditor : SelectableEditor
    {
        private SerializedProperty sortTypeProp;
        private SerializedProperty optionsProp;
        private SerializedProperty labelTextProp;
        private SerializedProperty labelImageProp;
        private SerializedProperty multipleChoicesLabelImageProp;
        private SerializedProperty dropdownTemplateProp;
        private SerializedProperty itemTextProp;
        private SerializedProperty itemImageProp;
        private SerializedProperty itemBgProp;
        private SerializedProperty maxSelectionCountToDisplayMoreTextProp;
        private SerializedProperty onSelectionChangedProp;

        // Text properties
        private SerializedProperty noSelectionTextProp;
        private SerializedProperty noSelectionTextKeyProp;
        private SerializedProperty selectionMergeTextProp;
        private SerializedProperty selectionMergeTextKeyProp;
        private SerializedProperty containsMoreTextProp;
        private SerializedProperty containsMoreTextKeyProp;

        // Toggle states for localization
        private bool useLocalizedNoSelectionText = false;
        private bool useLocalizedSelectionMergeText = false;
        private bool useLocalizedContainsMoreText = false;

        protected override void OnEnable()
        {
            base.OnEnable();

            // Get all properties
            sortTypeProp = serializedObject.FindProperty("optionsSortType");
            optionsProp = serializedObject.FindProperty("options");
            labelTextProp = serializedObject.FindProperty("labelText");
            labelImageProp = serializedObject.FindProperty("labelImage");
            multipleChoicesLabelImageProp = serializedObject.FindProperty("multipleChoicesLabelImage");
            dropdownTemplateProp = serializedObject.FindProperty("dropdownTemplate");
            itemTextProp = serializedObject.FindProperty("itemText");
            itemImageProp = serializedObject.FindProperty("itemImage");
            itemBgProp = serializedObject.FindProperty("itemBg");
            maxSelectionCountToDisplayMoreTextProp = serializedObject.FindProperty("maxSelectionCountToDisplayMoreText");
            onSelectionChangedProp = serializedObject.FindProperty("onSelectionChanged");

            // Text-related properties
            noSelectionTextProp = serializedObject.FindProperty("noSelectionText");
            noSelectionTextKeyProp = serializedObject.FindProperty("noSelectionTextKey");
            selectionMergeTextProp = serializedObject.FindProperty("selectionMergeText");
            selectionMergeTextKeyProp = serializedObject.FindProperty("selectionMergeTextKey");
            containsMoreTextProp = serializedObject.FindProperty("containsMoreText");
            containsMoreTextKeyProp = serializedObject.FindProperty("containsMoreTextKey");

            // Initialize toggle states based on whether the key fields have values
            useLocalizedNoSelectionText = !string.IsNullOrEmpty(noSelectionTextKeyProp.stringValue);
            useLocalizedSelectionMergeText = !string.IsNullOrEmpty(selectionMergeTextKeyProp.stringValue);
            useLocalizedContainsMoreText = !string.IsNullOrEmpty(containsMoreTextKeyProp.stringValue);
        }

        public override void OnInspectorGUI()
        {
            var indent = EditorGUI.indentLevel;
            
            serializedObject.Update();
            
            // Draw Selectable properties
            base.OnInspectorGUI();
            
            // Draw the MultiSelectDropdown properties
            
            EditorGUILayout.LabelField("Options", EditorStyles.boldLabel);
            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(sortTypeProp);
            EditorGUILayout.PropertyField(optionsProp);
            EditorGUI.indentLevel = indent;
            
            EditorGUILayout.LabelField("Label", EditorStyles.boldLabel);
            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(labelTextProp);
            EditorGUILayout.PropertyField(labelImageProp);
            EditorGUILayout.PropertyField(multipleChoicesLabelImageProp);
            EditorGUI.indentLevel = indent;

            EditorGUILayout.LabelField("Template", EditorStyles.boldLabel);
            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(dropdownTemplateProp);
            EditorGUILayout.PropertyField(itemTextProp);
            EditorGUILayout.PropertyField(itemImageProp);
            EditorGUILayout.PropertyField(itemBgProp);
            EditorGUI.indentLevel = indent;

            EditorGUILayout.LabelField("Label Configuration", EditorStyles.boldLabel);
            EditorGUI.indentLevel++;

            // No Selection Text
            DrawLocalizedField(
                ref useLocalizedNoSelectionText,
                "No Selection Text",
                noSelectionTextProp,
                noSelectionTextKeyProp
            );

            // Selection Merge Text
            DrawLocalizedField(
                ref useLocalizedSelectionMergeText,
                "Selection Merge Text",
                selectionMergeTextProp,
                selectionMergeTextKeyProp
            );

            // Contains More Text
            DrawLocalizedField(
                ref useLocalizedContainsMoreText,
                "Contains More Text",
                containsMoreTextProp,
                containsMoreTextKeyProp
            );

            EditorGUILayout.PropertyField(maxSelectionCountToDisplayMoreTextProp);
            EditorGUI.indentLevel = indent;

            EditorGUILayout.LabelField("Events", EditorStyles.boldLabel);
            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(onSelectionChangedProp);
            EditorGUI.indentLevel = indent;

            serializedObject.ApplyModifiedProperties();
        }

        private void DrawLocalizedField(ref bool useLocalized, string label, SerializedProperty textProp, SerializedProperty keyProp)
        {
            EditorGUILayout.BeginHorizontal();
            
            EditorGUILayout.LabelField(label, GUILayout.Width(EditorGUIUtility.labelWidth));
            useLocalized = GUILayout.Toggle(useLocalized, GUIContent.none, GUILayout.Width(15));
            EditorGUILayout.LabelField("Use Localization", GUILayout.Width(130));
            
            EditorGUILayout.EndHorizontal();

            EditorGUI.indentLevel++;
            if (useLocalized)
            {
                EditorGUILayout.PropertyField(keyProp, new GUIContent("Key"));
                textProp.stringValue = string.Empty;
            }
            else
            {
                EditorGUILayout.PropertyField(textProp, new GUIContent("Text"));
                keyProp.stringValue = string.Empty;
            }
            EditorGUI.indentLevel--;
            
            EditorGUILayout.Space(7);
        }
    }
}
