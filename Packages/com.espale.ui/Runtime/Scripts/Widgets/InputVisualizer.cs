using System;
using UnityEngine;
using UnityEngine.UI;
using Espale.Utilities;

namespace Espale.UI
{
    [ExecuteInEditMode, RequireComponent(typeof(Image))]
    public class InputVisualizer : MonoBehaviour
    {
        [Header("Behaviours")]
        public bool autoPickGamepad;
        public GamepadPlatform currentGamepadPlatform = GamepadPlatform.Xbox;
        
        [Header("Styles")]
        public InputVisualizerStyle playstationStyle;
        public InputVisualizerStyle xboxStyle;
        public InputVisualizerStyle nintendoStyle;
        public Sprite keyboardSprite;
        [Space]
        public GamepadKey key;

        private Image image;

        private void Awake() => image = GetComponent<Image>();
        private void OnEnable() => UpdateImage();

        private void Update()
        {
            if (autoPickGamepad)
                AutoPickGamepad();
            
#if UNITY_EDITOR
            if(!Application.isPlaying)
                UpdateImage();
#endif
        }

        public void UpdateGamepad(GamepadPlatform desiredPlatform)
        {
            if (currentGamepadPlatform == desiredPlatform) return;
            
            currentGamepadPlatform = desiredPlatform;
            UpdateImage();
        }

        private void UpdateImage()
        {
            var styleToUse = currentGamepadPlatform switch
            {
                GamepadPlatform.Playstation => playstationStyle,
                GamepadPlatform.Nintendo => nintendoStyle,
                GamepadPlatform.Xbox => xboxStyle,
                _ => null
            };
            
            image.sprite = styleToUse ? styleToUse.GetSpriteOfKey(key) : keyboardSprite;
        }

        private void AutoPickGamepad() => UpdateGamepad(InputUtilities.GetGamepadPlatform());
    }
}
