using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Espale.UI.Layers
{
    // This class could actually be a static class but when we store it as a MonoBehavior, we can directly use it's prefab
    // to call these methods from UI events.
    public class UILayerManager : MonoBehaviour
    {
        public static readonly Stack<UILayer> activeLayers = new ();

        /// <summary>
        /// Focuses on the given layer. If there exists other active layer(s), makes those layers inactive if they are >= in
        /// terms of layers, else makes them sleep.
        /// </summary>
        /// <param name="layer">Layer to focus to</param>
        public static void FocusLayer(UILayer layer)
        {
            if (activeLayers.Count < layer.layerOrder)
            {
                var layerDiff = layer.layerOrder - activeLayers.Count;
                
                // If there is an active layer present, set it to sleep mode.
                if (activeLayers.Count > 0)
                    SwitchState(activeLayers.Peek(), UILayer.UILayerState.Sleep);
                
                // Fill in the gap of layers with nulls if there is a gap (we subtract 1 as that's going to be our actual layer).
                for (var i = 0; i < layerDiff - 1; i++) activeLayers.Push(null);
            }
            else if (activeLayers.Count == layer.layerOrder)
            {
                // Disable the previous focused layer
                var prevFocusedLayer = activeLayers.Pop();
                SwitchState(prevFocusedLayer, UILayer.UILayerState.Inactive);
            }
            else
            {
                var layerDiff = activeLayers.Count - layer.layerOrder;

                // Disable all the extra layers.
                for (var i = 0; i < layerDiff + 1; i++)
                {
                    var prevFocusedLayer = activeLayers.Pop();
                    SwitchState(prevFocusedLayer, UILayer.UILayerState.Inactive);
                }
            }
            
            // Focus the layer and push it to the stack.
            activeLayers.Push(layer);
            SwitchState(layer, UILayer.UILayerState.Focused);
            
            AdjustInteractivites();
        }

        /// <summary>
        /// Hides the given layer and every other layer above it, if any.
        /// </summary>
        /// <param name="layer">Layer to hide</param>
        public static void HideLayer(UILayer layer)
        {
            if (!activeLayers.Contains(layer)) return;

            while (activeLayers.Count > 0)
            {
                var topLayer = activeLayers.Peek(); // Cache the top layer.
                HideTopLayer(); // Hide the top layer.
                if (topLayer == layer) return; // If the hid top layer is the desired layer, terminate.
            }
        }

        /// <summary>
        /// If the given layer is not inactive, uses `UILayerManager.HideLayer()` method on the layer, else, it applies
        /// the `UILayerManager.FocusLayer()` method.
        /// </summary>
        /// <param name="layer">Target layer</param>
        public static void ToggleLayer(UILayer layer)
        {
            if (layer.state is not UILayer.UILayerState.Inactive) HideLayer(layer);
            else FocusLayer(layer);
        }
        
        /// <summary>
        /// Hides the layer at the top of the active layers stack.
        /// </summary>
        public static void HideTopLayer()
        {
            if (activeLayers.Count <= 0) return;
            
            // Hide the top layer.
            var topLayer = activeLayers.Pop();
            SwitchState(topLayer, UILayer.UILayerState.Inactive);
            
            // Pop the layers containing null, if there are any, until reaching a valid layer.
            while (activeLayers.Count > 0 && !activeLayers.Peek())
                activeLayers.Pop();
            
            // If there is a layer left, focus it.
            if (activeLayers.Count > 0)
                SwitchState(activeLayers.Peek(), UILayer.UILayerState.Focused);

            AdjustInteractivites();
        }

        private static void AdjustInteractivites()
        {
            var layers = activeLayers.Reverse().Where(l => l != null).ToArray();
            if (layers.Length < 2) return;
            
            for (var i = 0; i < layers.Length - 1; i++)
            {
                var layer = layers[i];
                var layerAbove = layers[i + 1];
                
                SwitchState(layer, layer.state, layerAbove.blockLayersBelow);
            }
        }

        /// <summary>
        /// Switches the state of the given <c>UILayer</c>, checks for <c>null</c> values.
        /// </summary>
        /// <param name="layer">Layer to change the state of</param>
        /// <param name="state">Desired state for the layer</param>
        /// <param name="interactivityLimitedByAbove">Determines where the layers above are blocking the interactivity</param>
        private static void SwitchState(UILayer layer, UILayer.UILayerState state, bool interactivityLimitedByAbove=true)
        {
            if (layer) 
                layer.SwitchState(state, interactivityLimitedByAbove);
        }
    }
}
