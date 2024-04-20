using Espale.Utilities;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Espale.Localization
{
    [ExecuteInEditMode]
    public class TextLocalizer : MonoBehaviour
    {
        [LocalizedKey] public string key;
        [Tooltip("Should an error message get displayed when there is no value for the given key, if set to false will just remain empty.")]
        public bool displayErrorMessage = true;
        public bool useFallBackLanguage = true;
        public Language fallbackLanguage = Language.English;

        public string prefix;
        public string suffix;
        
        private TextMeshProUGUI textTMP;
        private Text text;

        private void Awake()
        {
            textTMP = GetComponent<TextMeshProUGUI>();
            text = GetComponent<Text>();
        }

        private void OnEnable()
        {
            UpdateLanguage();
            Localizator.OnLanguageChange += UpdateLanguage;
        }

        private void OnDisable() => Localizator.OnLanguageChange -= UpdateLanguage;

        /// <summary>
        /// Forces an update for the text. No need for manuel activation as it listens to <c>Localizator.OnLanguageChange</c>
        /// </summary>
        public void UpdateLanguage()
        {
            if (!text && !textTMP)
            {
                text = GetComponent<Text>();
                textTMP = GetComponent<TextMeshProUGUI>();
                
#if UNITY_EDITOR
                if (!text && !textTMP)
                    BetterDebug.LogWarning("TextLocalizer component requires either Text or TMP component to update the text.");
#endif
            }

            var localizedText = useFallBackLanguage
                ? Localizator.GetString(key, fallbackLanguage, displayErrorMessage)
                : Localizator.GetStringWithCurrentLanguage(key, displayErrorMessage);
            var content = prefix + localizedText + suffix;
            
            if (text) text.text = content;
            if (textTMP) textTMP.text = content;
        }
    }
}
