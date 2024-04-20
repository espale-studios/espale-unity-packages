using System.Collections.Generic;
using Espale.Utilities;

namespace Espale.UI.Layers
{
    public class UILayerManager : Singleton<UILayerManager>
    {
        public static Stack<UILayer> activeLayers = new ();

        public static void FocusLayer(UILayer layer) => GetInstance().FocusLayerFromInstance(layer);
        private void FocusLayerFromInstance(UILayer layer)
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
        }

        public static void HideLayer() => GetInstance().HideTopLayerFromInstance();
        private void HideTopLayerFromInstance()
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
        }

        /// <summary>
        /// Switches the state of the given <c>UILayer</c>, checks for <c>null</c> values.
        /// </summary>
        /// <param name="layer">Layer to change the state of</param>
        /// <param name="state">Desired state for the layer</param>
        private static void SwitchState(UILayer layer, UILayer.UILayerState state)
        {
            if (layer) 
                layer.SwitchState(state);
        }
    }
}
