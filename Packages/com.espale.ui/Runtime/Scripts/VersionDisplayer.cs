using Espale.Localization;
using Espale.Utilities;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Espale.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class VersionDisplayer : Singleton<VersionDisplayer>
    {
        private TMP_Text versionText;
        private RawImage bg;

        [Header("Behaviour")]
        [SerializeField] private KeyCode toggleKey = KeyCode.None;
        [SerializeField] private bool activeOnStart = true;
        
        [Header("Visuals")]
        [SerializeField] private Color bgColor = Color.red;
        [SerializeField, LocalizedKey] private string prefixKey;

        private CanvasGroup canvasGroup;
        
        private new void Awake()
        { 
            base.Awake();

            bg = transform.GetChild(0).GetChild(0).GetComponent<RawImage>();
            versionText = bg.GetComponentInChildren<TMP_Text>();

            canvasGroup = GetComponent<CanvasGroup>();
            canvasGroup.alpha = activeOnStart ? 1f : 0f;
            
            UpdateText();
            UpdateColor(bgColor);
        }

        private new void OnEnable()
        {
            base.OnEnable();
            Localizator.OnLanguageChange += UpdateText;
        }

        private new void OnDisable()
        {
            base.OnDisable();
            Localizator.OnLanguageChange -= UpdateText;
        }

        private void Update()
        {
            if (Input.GetKeyDown(toggleKey))
                canvasGroup.alpha = canvasGroup.alpha > 0f ? 0f : 1f;
        }

        private void UpdateText()
            => versionText.text = Localizator.GetString(prefixKey, returnErrorString: false) + " " + Application.version;
        private void UpdateColor(Color color) => bg.color = color;
    }
}
