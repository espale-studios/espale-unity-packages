using Espale.Utilities;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

namespace Espale.UI
{
    public class FPSDisplayer : Singleton<FPSDisplayer>
    {
        private static float avg = 0;
        private static float sceneAvg = 0;
        private static int sceneFrameCount = 0;

        [Header("Toggle")]
        [SerializeField] private bool toggled = true;
        [SerializeField] private KeyCode toggleKey = KeyCode.F6;

        [Header("Display Options")] 
        [SerializeField] private bool displayAverage = true;
        [SerializeField] private bool displaySceneSpecific = true;
        [Space]
        [SerializeField] private Color textColor = Color.green;
        private TMP_Text fpsText;

        private new void Awake()
        {
            base.Awake();

            fpsText = GetComponentInChildren<TMP_Text>();
            UpdateColor(textColor);
        }

        protected override void OnSceneChange(Scene current, Scene next)
        {
            base.OnSceneChange(current, next);
            
            sceneAvg = 0f;
            sceneFrameCount = 0;
        }

        private void Update()
        {
            if (!fpsText || Time.unscaledDeltaTime == 0f) return;

            if (Input.GetKeyDown(toggleKey)) toggled = !toggled;

            var currentFPS = 1f / Time.unscaledDeltaTime;
            var roundedFPS = Mathf.RoundToInt(currentFPS);

            sceneFrameCount++;

            avg = (avg * (Time.frameCount - 1) + currentFPS) / Time.frameCount;
            sceneAvg = (sceneAvg * (sceneFrameCount - 1) + currentFPS) / sceneFrameCount;

            if (!toggled)
            {
                fpsText.text = "";
                return;
            }

            var textToDisplay = $"FPS: {(roundedFPS < 100 ? roundedFPS + " " : roundedFPS.ToString())}";
            if (displayAverage)
            {
                textToDisplay += $" | Avg: {Mathf.RoundToInt(avg)}";
                if (displaySceneSpecific)
                    textToDisplay += $" | Scene Avg: {Mathf.RoundToInt(sceneAvg)}";
            }

            fpsText.text = textToDisplay;
        }

        public void UpdateColor(Color color)
        {
            textColor = color;
            fpsText.color = color;
        }
    }
}
