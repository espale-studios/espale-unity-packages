using System;
using System.Linq;
using Espale.Utilities;
using UnityEngine;
using NaughtyAttributes;
using UnityEngine.UI;

namespace Espale.UI.Layers
{
    [RequireComponent(typeof(CanvasGroup))]
    public class UILayer : MonoBehaviour
    {
        [Header("Layer Settings")] 
        [MinValue(1), Tooltip("Order of this layer, where 1 is the bottom layer.")] public int layerOrder = 1;
        [Tooltip("If set to true, the layer will be activated when the scene loads. Note that having more than" +
                 " one UI Layer with this parameter set to true will cause an error")]
        public bool defaultActiveLayer = false;
        public bool blockLayersBelow = true;
        [Tooltip("This widget will be selected when this layer gets focused.")]
        public Selectable defaultSelectable;

        [HideInInspector] public UILayerState state = UILayerState.Inactive;

        private bool defaultInteractable;
        private bool defaultBlocksRaycasts;

        protected CanvasGroup canvasGroup;
        protected Canvas canvas;

        protected void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            canvas = GetComponentInParent<Canvas>();

            defaultInteractable = canvasGroup.interactable;
            defaultBlocksRaycasts = canvasGroup.blocksRaycasts;

            canvas.sortingOrder = layerOrder;
            
            if (!defaultActiveLayer)
                SwitchState(state, ignoreStateChangeChecks: true);
            else
            {
#if UNITY_EDITOR
                // If there are multiple layers to activate at start, assert an error message.
                var layers = FindObjectsOfType<UILayer>().Where(l => l.defaultActiveLayer).ToArray();
                if (layers.Length > 1)
                    BetterDebug.LogError(
                        "There are multiple <i>UI Layers</i> with <b>defaultActiveWindow</b> set to <i>true</i>, <b>at most one can exist per scene.</b> " +
                        $"Layers causing the error:\n{layers.Aggregate("", (s, layer) => $"{s}{layer.gameObject.name}\n")}"
                    );
#endif
                UILayerManager.FocusLayer(this);
            }
        }

        protected void Update() => canvasGroup.ignoreParentGroups = true;

        public void HideIfFocused()
        {
            if (state is not UILayerState.Focused) return;
            Hide();
        }
        
        public void Hide() => UILayerManager.HideLayer(this);
        public void Focus() => UILayerManager.FocusLayer(this);

        /// <summary>
        /// This method will be called to change the visibility of the canvas. This method can be overriden to apply custom animations.
        /// </summary>
        protected virtual void ChangeVisibility(bool visible)
        {
            canvasGroup.alpha = visible ? 1f : 0f;
        }
        
        internal void SwitchState(UILayerState desiredState, bool blockedFromAbove=true, bool ignoreStateChangeChecks=false)
        {
            var stateChanged = state != desiredState;
            state = desiredState;

            var changeVisibility = stateChanged || ignoreStateChangeChecks;

            switch (state)
            {
                case UILayerState.Inactive:
                    if (changeVisibility) ChangeVisibility(false);
                    
                    canvasGroup.interactable = !blockedFromAbove;
                    canvasGroup.blocksRaycasts = !blockedFromAbove;
                    break;
                case UILayerState.Sleep:
                    if (changeVisibility) ChangeVisibility(true);
                    
                    canvasGroup.interactable = !blockedFromAbove;
                    canvasGroup.blocksRaycasts = !blockedFromAbove;
                    break;
                case UILayerState.Focused:
                    if (changeVisibility) ChangeVisibility(true);
                    
                    canvasGroup.interactable = defaultInteractable;
                    canvasGroup.blocksRaycasts = defaultBlocksRaycasts;
                    
                    if (defaultSelectable) defaultSelectable.Select();
                    break;
            }
        }

        /// <summary>
        /// Defines the state of a UI Layer.
        /// </summary>
        public enum UILayerState : byte
        {
            /// <summary>
            /// The layer is inactive, meaning that it's not visible nor interactable.
            /// </summary>
            Inactive = 0,
            /// <summary>
            /// The layer is in sleep mode, meaning that it may be visible but it's not interactable and not the primary focus.
            /// </summary>
            Sleep = 1,
            /// <summary>
            /// The layer is focused, meaning that it's the primary layer of the UI.
            /// </summary>
            Focused = 2,
        }
    }
}
