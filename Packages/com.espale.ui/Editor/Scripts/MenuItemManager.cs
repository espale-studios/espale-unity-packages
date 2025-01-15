using UnityEditor;
using UnityEngine;

namespace Espale.UI.Editor
{
    public static class MenuItemManager
    {
        [MenuItem("Espale Studios/UI/Layers/Blank Layer", priority=22), MenuItem("GameObject/Espale UI/Layers/Blank Layer", priority=91)]
        public static void AddBlankLayer() => InstantiateObject("Blank UI Layer", "Blank Layer");
        
#region Section 1
        [MenuItem("Espale Studios/UI/Widgets/Button", priority=11), MenuItem("GameObject/Espale UI/Widgets/Button", priority=50)]
        public static void AddButton() => InstantiateObject("Espale Button", "Espale Button");
        
        [MenuItem("Espale Studios/UI/Widgets/Toggle", priority=11), MenuItem("GameObject/Espale UI/Widgets/Toggle", priority=51)]
        public static void AddToggle() => InstantiateObject("Espale Toggle", "Espale Toggle");
#endregion

#region Section 2
        
        [MenuItem("Espale Studios/UI/Widgets/Multi Select Dropdown with Icon", priority = 22), MenuItem("GameObject/Espale UI/Widgets/Multi Select Dropdown with Icon", priority = 71)]
        public static void AddMultiSelectDropdown() => InstantiateObject("MultiSelectDropdown", "Multi Select Dropdown with Icon");   
        
        [MenuItem("Espale Studios/UI/Widgets/Multi Select Dropdown", priority = 22), MenuItem("GameObject/Espale UI/Widgets/Multi Select Dropdown", priority = 72)]
        public static void AddMultiSelectDropdownNoIcon() => InstantiateObject("MultiSelectDropdown - No Icon", "Multi Select Dropdown");
        
        [MenuItem("Espale Studios/UI/Widgets/Input Visualizer", priority = 22), MenuItem("GameObject/Espale UI/Widgets/Input Visualizer", priority = 73)]
        public static void AddInputVisualizer() => InstantiateObject("InputVisualizer", "Input Visualizer");
        
        [MenuItem("Espale Studios/UI/Widgets/Linear Progress Bar", priority=0), MenuItem("GameObject/Espale UI/Widgets/Linear Progress Bar", priority=74)]
        public static void AddLinearProgressBar() => InstantiateObject("Linear Progress Bar", "Linear Progress Bar");

        [MenuItem("Espale Studios/UI/Widgets/Radial Progress Bar", priority=0), MenuItem("GameObject/Espale UI/Widgets/Radial Progress Bar", priority=75)]
        public static void AddRadialProgressBar() => InstantiateObject("Radial Progress Bar", "Radial Progress Bar");
#endregion   

#region Section 3
        [MenuItem("Espale Studios/UI/Widgets/FPS Displayer", priority=22), MenuItem("GameObject/Espale UI/Widgets/FPS Displayer", priority=91)]
        public static void AddFPSDisplayer() => InstantiateObject("FPS Displayer", "FPS Displayer");
        
        [MenuItem("Espale Studios/UI/Widgets/Version Displayer", priority=22), MenuItem("GameObject/Espale UI/Widgets/Version Displayer", priority=92)]
        public static void AddVersionDisplayer() => InstantiateObject("Version Displayer", "Version Displayer");
#endregion 

        private static void InstantiateObject(string resourceName, string name)
        {
            var obj = CanvasPrefabInstantiator.InstantiatePrefab(Resources.Load<GameObject>(resourceName));
            obj.name = name;
            
            Selection.SetActiveObjectWithContext(obj, null);
        }
    }
}
