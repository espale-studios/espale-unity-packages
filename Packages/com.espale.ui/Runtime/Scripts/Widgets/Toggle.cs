using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Espale.UI
{
    [ExecuteInEditMode]
    public class Toggle : Selectable, ISubmitHandler
    {
        public bool isOn = false;

        [FormerlySerializedAs("OnToggle")] public UnityEvent onToggle;

        [FormerlySerializedAs("fillTransitionDuration"),
        Header("Filling"), Range(1f, 20f)] public float fillTransitionSpeed = 8f;
        private float currentFillTransitionSpeed;
        public Color fillColor = Color.red;
        public ToggleFillAnimation animType;
        [Range(0, 1)] public float fillScale = .8f;

        private Image mask;
        private Image fill;

        private RectTransform maskRect;

        private void Awake()
        {
            mask = transform.Find("Mask").GetComponent<Image>();
            maskRect = mask.GetComponent<RectTransform>();

            fill = mask.transform.Find("Fill").GetComponent<Image>();
        }

        private void Update()
        {
#if UNITY_EDITOR
            if (Application.isEditor && !Application.isPlaying)
                fill.color = fillColor;
#endif

            UpdateFill();

            if (isOn)
                fill.color = Color.Lerp(fill.color, fillColor,currentFillTransitionSpeed * Time.unscaledDeltaTime);
        }

        private void UpdateFill()
        {
            fillScale = Mathf.Clamp(fillScale, 0f, 1f);

            if (maskRect) maskRect.localScale = Vector2.one * fillScale;
            switch (animType)
            {
                case ToggleFillAnimation.Horizontal:
                    currentFillTransitionSpeed = fillTransitionSpeed;
                    mask.fillMethod = Image.FillMethod.Horizontal;
                    break;
                case ToggleFillAnimation.Vertical:
                    currentFillTransitionSpeed = fillTransitionSpeed;
                    mask.fillMethod = Image.FillMethod.Vertical;
                    break;
                case ToggleFillAnimation.Radial90:
                    currentFillTransitionSpeed = fillTransitionSpeed;
                    mask.fillMethod = Image.FillMethod.Radial90;
                    break;
                case ToggleFillAnimation.Radial180:
                    currentFillTransitionSpeed = fillTransitionSpeed;
                    mask.fillMethod = Image.FillMethod.Radial180;
                    break;
                case ToggleFillAnimation.Radial360:
                    currentFillTransitionSpeed = fillTransitionSpeed;
                    mask.fillMethod = Image.FillMethod.Radial360;
                    break;
                case ToggleFillAnimation.Fade:
                    currentFillTransitionSpeed = fillTransitionSpeed;
                    mask.fillAmount = 1;
                    if (!isOn)
                        fill.color = Color.Lerp(fill.color, Color.clear, currentFillTransitionSpeed * Time.unscaledDeltaTime);
                    break;
                case ToggleFillAnimation.None:
                    currentFillTransitionSpeed = 0f;
                    break;
            }

            if (animType != ToggleFillAnimation.Fade)
                mask.fillAmount = Mathf.Lerp(mask.fillAmount, isOn ? 1 : 0, currentFillTransitionSpeed * Time.unscaledDeltaTime);
        }

        private void Click()
        {
            if (!interactable) return;
            
            isOn = !isOn;
            onToggle.Invoke();
        }

#region Cursor Detection

        public void OnSubmit(BaseEventData eventData) => Click();

        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);
            Click();
        }

#endregion
    }

    public enum ToggleFillAnimation : byte
    {
        Horizontal = 0,
        Vertical = 1,
        Radial90 = 2,
        Radial180 = 3,
        Radial360 = 4,
        Fade = 5,
        None = 6,
    }
}
