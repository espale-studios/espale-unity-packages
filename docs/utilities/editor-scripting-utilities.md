# EditorScriptingUtilities

`EditorScriptingUtilities` (assembly `Espale.Utilities.Editor`, namespace `Espale.Utilities`) has a few IMGUI shortcuts for custom inspectors and editor windows. It's editor-only, so use it from scripts in an `Editor` folder or an editor assembly.

```csharp
using Espale.Utilities;
using UnityEditor;

[CustomEditor(typeof(MyComponent))]
public class MyComponentEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        EditorScriptingUtilities.HorizontalLine();          // 1 px, ColorUtilities.darkGray
        EditorScriptingUtilities.HorizontalLine(Color.red, 2);

        EditorScriptingUtilities.BeginCenter();
        if (GUILayout.Button("Do something", GUILayout.Width(160))) { /* ... */ }
        EditorScriptingUtilities.EndCenter();
    }
}
```

| Method | Description |
| --- | --- |
| `HorizontalLine(int height = 1)` | Draws a horizontal line in `ColorUtilities.darkGray`. |
| `HorizontalLine(Color color, int height = 1)` | Draws a horizontal line in the given color. |
| `BeginCenter()` | Starts a horizontal group with flexible space before its content. Always pair it with `EndCenter()`. |
| `EndCenter()` | Adds flexible space after the content and closes the group, so the content is centered. |

## Script icons

The package also ships Inspector and Project window icons for its components. They are applied automatically.
