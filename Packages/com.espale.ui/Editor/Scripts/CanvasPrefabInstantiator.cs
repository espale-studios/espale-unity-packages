using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Espale.UI.Editor
{
    public class CanvasPrefabInstantiator
    {
        /// <summary>
        /// Instantiates the given UI <c>Prefab</c> in the correct position and makes sure it is inside a <c>Canvas</c>
        /// </summary>
        /// <param name="prefab"><c>Prefab</c> to instantiate</param>
        /// <returns>Instantiated object</returns>
        public static GameObject InstantiatePrefab(GameObject prefab)
        {
            var doesPrefabContainCanvas = prefab.GetComponentInParent<Canvas>() || prefab.GetComponent<Canvas>();

            if (!prefab)
            {
                Debug.LogError("Tried to instantiate a null object.");
                return null;
            }
            
            var obj = Object.Instantiate(prefab, !doesPrefabContainCanvas ? FindCanvasParent() : GetSelectedParent(), false);
            return obj;
        }

        /// <summary>
        /// Returns the selected <c>GameObject</c>'s <c>Transform</c> if it exists, else returns <c>null</c>.
        /// </summary>
        /// <returns>Parent <c>Transform</c></returns>
        private static Transform GetSelectedParent() => Selection.activeGameObject ? Selection.activeGameObject.transform : null;
        
        /// <summary>
        /// If the selected <c>GameObject</c> is not in a <c>Canvas</c> or not a <c>Canvas</c>, creates a <c>Canvas</c> to be the parent.
        /// </summary>
        /// <returns>Parent <c>Canvas</c>, or canvas element.</returns>
        private static Transform FindCanvasParent()
        {
            var selection = Selection.activeGameObject;
            if (selection)
            {
                var parentCanvas = selection.GetComponentInParent<Canvas>();
                // If the selected object is inside a Canvas or is a Canvas, make the selection parent.
                if (parentCanvas)
                    return selection.transform;
            }
            
            // Create a canvas to be the parent.
            var canvas = CreateBlankCanvas();

            // If there is a selection, make it the parent of the new canvas.
            if (selection) canvas.transform.parent = selection.transform;

            return canvas.transform;
        }

        /// <summary>
        /// Creates a default <c>Canvas</c> Unity creates when you create a UI element outside of an existing <c>Canvas</c>.
        /// </summary>
        /// <returns>the blank <c>Canvas</c></returns>
        private static GameObject CreateBlankCanvas()
        {
            var canvas = new GameObject { name = "Canvas", layer = 5 }; // layer 5 is the UI layer.
            canvas.AddComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
            canvas.AddComponent<CanvasScaler>().uiScaleMode = CanvasScaler.ScaleMode.ConstantPixelSize;
            canvas.AddComponent<GraphicRaycaster>();

            return canvas;
        }
    }
}
