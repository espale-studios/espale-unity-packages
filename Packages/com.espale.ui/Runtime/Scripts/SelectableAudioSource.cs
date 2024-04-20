using Espale.Utilities;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Espale.UI
{
    [RequireComponent(typeof(Selectable))]
    public class SelectableAudioSource : MonoBehaviour, ISelectHandler, IPointerClickHandler, IPointerEnterHandler, ISubmitHandler
    {
        [Header("Audio")] 
        public AudioUtilities.AudioData highlightedAudioData;
        public AudioUtilities.AudioData selectedAudioData;
        public AudioUtilities.AudioData pressedAudioData;
        [Space]
        public AudioMixerGroup mixerGroup;
        
        private Selectable selectable;

        private void Awake() => selectable = GetComponent<Selectable>();

        private void OnSelect()
        {
            if (!selectable.interactable) return;
            UIAudioManager.PlayOneShotAudio(selectedAudioData.clip, selectedAudioData.volume, mixerGroup);
        }
        
        private void OnHighlight()
        {
            if (!selectable.interactable) return;
            UIAudioManager.PlayOneShotAudio(highlightedAudioData.clip, highlightedAudioData.volume, mixerGroup);
        }
        
        private void OnClick()
        {
            if (!selectable.interactable) return;
            UIAudioManager.PlayOneShotAudio(pressedAudioData.clip, pressedAudioData.volume, mixerGroup);
        }
        
        void ISelectHandler.OnSelect(BaseEventData eventData) => OnSelect();
        void IPointerClickHandler.OnPointerClick(PointerEventData eventData) => OnClick();
        void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData) => OnHighlight();
        void ISubmitHandler.OnSubmit(BaseEventData eventData) => OnClick();
    }

}
