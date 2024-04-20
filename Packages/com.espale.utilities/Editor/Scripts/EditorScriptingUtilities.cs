using UnityEngine;
using UnityEditor;

namespace Espale.Utilities
{
    public static class EditorScriptingUtilities
    {
        /// <summary>
        /// Draws a dark gray horizontal line.
        /// </summary>
        /// <param name="height">height of the line to draw</param>
        public static void HorizontalLine(int height = 1) => HorizontalLine(ColorUtilities.darkGray, height);

        /// <summary>
        /// Draws a horizontal line.
        /// </summary>
        /// <param name="height">height of the line to draw</param>
        /// <param name="color">color of the line to draw</param>
        public static void HorizontalLine(Color color, int height = 1)
        {
            var rect = EditorGUILayout.GetControlRect(false, height);
            rect.height = height;

            EditorGUI.DrawRect(rect, color);
        }

        /// <summary>
        /// Must be followed by the <c>EndCenter()</c> function.
        /// </summary>
        public static void BeginCenter()
        {
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
        }

        /// <summary>
        /// Must be used after the <c>BeginCenter()</c> function.
        /// </summary>
        public static void EndCenter()
        {
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }
    }
}
