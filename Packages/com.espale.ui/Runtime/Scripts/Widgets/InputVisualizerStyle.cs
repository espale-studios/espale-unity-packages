using System;
using Espale.Utilities;
using UnityEngine;

namespace Espale.UI
{
    [CreateAssetMenu(fileName = "new Input Visualizer Style", menuName = "Espale Studios/UI/Input Visualizer Style",
        order = 1)]
    public class InputVisualizerStyle : ScriptableObject
    {
        [Header("Triggers")] public Sprite rightTrigger;
        public Sprite leftTrigger;

        [Header("Bumpers")] public Sprite rightBumper;
        public Sprite leftBumper;

        [Header("Sticks")] public Sprite leftStick;
        public Sprite leftStickButton;
        public Sprite rightStick;
        public Sprite rightStickButton;

        [Header("Dpad")] public Sprite dpad;
        public Sprite dpadTop;
        public Sprite dpadBottom;
        public Sprite dpadRight;
        public Sprite dpadLeft;

        [Header("Buttons")] public Sprite topButton;
        public Sprite bottomButton;
        public Sprite leftButton;
        public Sprite rightButton;

        public Sprite GetSpriteOfKey(GamepadKey key)
        {
            switch (key)
            {
                case GamepadKey.RightTrigger:
                    return rightTrigger;
                case GamepadKey.LeftTrigger:
                    return leftTrigger;
                case GamepadKey.RightBumper:
                    return rightBumper;
                case GamepadKey.LeftBumper:
                    return leftBumper;
                case GamepadKey.LeftStick:
                    return leftStick;
                case GamepadKey.LeftStickButton:
                    return leftStickButton;
                case GamepadKey.RightStick:
                    return rightStick;
                case GamepadKey.RightStickButton:
                    return rightStickButton;
                case GamepadKey.Dpad:
                    return dpad;
                case GamepadKey.DpadTop:
                    return dpadTop;
                case GamepadKey.DpadBottom:
                    return dpadBottom;
                case GamepadKey.DpadRight:
                    return dpadRight;
                case GamepadKey.DpadLeft:
                    return dpadLeft;
                case GamepadKey.TopButton:
                    return topButton;
                case GamepadKey.BottomButton:
                    return bottomButton;
                case GamepadKey.LeftButton:
                    return leftButton;
                case GamepadKey.RightButton:
                    return rightButton;
                default:
                    throw new ArgumentOutOfRangeException(nameof(key), key, null);
            }
        }
    }
}
