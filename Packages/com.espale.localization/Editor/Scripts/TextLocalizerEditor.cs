using Espale.Utilities;
using UnityEditor;
using UnityEngine;

namespace Espale.Localization.Backend
{
    [CustomEditor(typeof(TextLocalizer))]
    public class TextLocalizerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("key"), new GUIContent("Key"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("displayErrorMessage"), new GUIContent("Display Error Message"));
            
            EditorScriptingUtilities.HorizontalLine();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("useFallBackLanguage"), new GUIContent("Use Fallback Language"));
            if (serializedObject.FindProperty("useFallBackLanguage").boolValue)
                EditorGUILayout.PropertyField(serializedObject.FindProperty("fallbackLanguage"), new GUIContent("Fallback Language"));
            EditorScriptingUtilities.HorizontalLine();
            
            EditorGUILayout.PropertyField(serializedObject.FindProperty("prefix"), new GUIContent("Prefix"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("suffix"), new GUIContent("Suffix"));
            
            serializedObject.ApplyModifiedProperties();
        }
    }
}
