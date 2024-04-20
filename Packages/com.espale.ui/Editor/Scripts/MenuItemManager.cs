using UnityEditor;
using UnityEngine;

namespace Espale.UI.Editor
{
    public static class MenuItemManager
    {
        [MenuItem("Espale Studios/UI/Layers/Blank Layer", priority=22), MenuItem("GameObject/Espale UI/Layers/Blank Layer", priority=91)]
        public static void AddBlankLayer() => InstantiateObject("Blank UI Layer", "New Blank Layer");
        
#region Section 1
        [MenuItem("Espale Studios/UI/Widgets/Button", priority=11), MenuItem("GameObject/Espale UI/Widgets/Button", priority=50)]
        public static void AddButton() => InstantiateObject("Espale Button", "New Espale Button");
        
        [MenuItem("Espale Studios/UI/Widgets/Toggle", priority=11), MenuItem("GameObject/Espale UI/Widgets/Toggle", priority=51)]
        public static void AddToggle() => InstantiateObject("Espale Toggle", "New Espale Toggle");
#endregion

#region Section 2
        [MenuItem("Espale Studios/UI/Widgets/Input Visualizer", priority = 22), MenuItem("GameObject/Espale UI/Widgets/Input Visualizer", priority = 71)]
        public static void AddInputVisualizer() => InstantiateObject("InputVisualizer", "New Input Visualizer");
        
        [MenuItem("Espale Studios/UI/Widgets/Linear Progress Bar", priority=0), MenuItem("GameObject/Espale UI/Widgets/Linear Progress Bar", priority=72)]
        public static void AddLinearProgressBar() => InstantiateObject("Linear Progress Bar", "New Linear Progress Bar");

        [MenuItem("Espale Studios/UI/Widgets/Radial Progress Bar", priority=0), MenuItem("GameObject/Espale UI/Widgets/Radial Progress Bar", priority=73)]
        public static void AddRadialProgressBar() => InstantiateObject("Radial Progress Bar", "New Radial Progress Bar");
#endregion   

#region Section 3
        [MenuItem("Espale Studios/UI/Widgets/FPS Displayer", priority=22), MenuItem("GameObject/Espale UI/Widgets/FPS Displayer", priority=91)]
        public static void AddFPSDisplayer() => InstantiateObject("FPS Displayer", "New FPS Displayer");
        
        [MenuItem("Espale Studios/UI/Widgets/Version Displayer", priority=22), MenuItem("GameObject/Espale UI/Widgets/Version Displayer", priority=92)]
        public static void AddVersionDisplayer() => InstantiateObject("Version Displayer", "New Version Displayer");
#endregion 

        private static void InstantiateObject(string resourceName, string name)
        {
            var obj = CanvasPrefabInstantiator.InstantiatePrefab(Resources.Load<GameObject>(resourceName));
            obj.name = name;
            
            Selection.SetActiveObjectWithContext(obj, null);
        }
    }
}
