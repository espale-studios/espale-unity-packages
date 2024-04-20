using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Espale.UI
{
    [RequireComponent(typeof(ScrollRect))]
    public class ScrollViewAutoScroller : MonoBehaviour
    {
        [Header("Config")]
        public bool isActive = true;
        public bool updateVerticalNavOnEnable = false;
        public bool loopableNavigation = true;
        [Tooltip("Making this true will search for Selectable components using FindComponentInChildren, meaning if an" +
                 "element in the ScrollView has a child that can be selected, it will be added to the list too.")]
        public bool useDepthBasedSearchForSelectables = false;

        [Header("Settings")]
        public bool useUnscaledTime = false;
        public float offset = 0f;
        [Range(1f, 100f)] public float autoScrollSpeed = 10f;

        private float desiredPosition = 0f;
        private Selectable[] children;
        private GameObject lastSelection;
        private ScrollRect scrollView;
        private RectTransform rectTransform;
        
        private void Awake()
        {
            scrollView = GetComponent<ScrollRect>();
            rectTransform = GetComponent<RectTransform>();
        }

        private void OnEnable()
        {
            UpdateChildren();
            
            if (updateVerticalNavOnEnable)
                UpdateVerticalNav();
        }
        
        private void Update()
        {
            if (!isActive) return;
            
            StepScrollPosition(useUnscaledTime ? Time.unscaledDeltaTime : Time.deltaTime);
            
            if (lastSelection != EventSystem.current.currentSelectedGameObject)
                TryScrollToSelectedIndex();
            
            lastSelection = EventSystem.current.currentSelectedGameObject;
        }

        public void StepScrollPosition(float dt, float speed)
            => scrollView.content.anchoredPosition = Vector2.up * GetScrollPosition(speed * dt);
        
        public void StepScrollPosition(float dt) => StepScrollPosition(dt, autoScrollSpeed);

        private float GetScrollPosition(float speed)
        {
            if (Mathf.Abs(scrollView.content.anchoredPosition.y - desiredPosition) < 1f)
                return desiredPosition;
            
            return Mathf.Lerp(
                scrollView.content.anchoredPosition.y,
                desiredPosition,
                speed
            );
        }
        
        public void UpdateChildren()
        {
            var selectables = new List<Selectable>();
            foreach (Transform child in transform.GetChild(0).GetChild(0))
            {
                var selectable = useDepthBasedSearchForSelectables ?
                    child.GetComponentInChildren<Selectable>() :
                    child.GetComponent<Selectable>();
                
                if (selectable)
                    selectables.Add(selectable);
            }

            children = selectables.ToArray();
        }

        public void UpdateVerticalNav()
        {
            for (var i = 0; i < children.Length; i++)
            {
                var nav = children[i].navigation;
                nav.mode = Navigation.Mode.Explicit;

                var selectOnDown = i + 1 == children.Length ? 0 : i + 1;
                if (loopableNavigation || selectOnDown != 0)
                    nav.selectOnDown = children[selectOnDown];
                
                var selectOnUp = i -1 == -1 ? children.Length - 1 : i - 1;
                if (loopableNavigation || selectOnUp != children.Length - 1)
                    nav.selectOnUp = children[selectOnUp];

                children[i].navigation = nav;
            }
        }

        public void TryScrollToSelectedIndex()
        {
            for (var i = 0; i < children.Length; i++)
            {
                if (!children[i])
                {
                    UpdateChildren();
                    return;
                }

                if (EventSystem.current.currentSelectedGameObject != children[i].gameObject) continue;
                
                ScrollToIndex(i);
                return;
            }
        }

        public void ScrollToIndex(int index)
        {
            var selectedTransform = children[index].transform;
            
            while (selectedTransform.parent.parent.parent != transform)
            {
                selectedTransform = selectedTransform.parent;
                if (!selectedTransform.parent.parent.parent)
                {
#if UNITY_EDITOR
                    Debug.LogError($"Could not find the relation of {children[index].transform.name} to the scroll view.");
#endif
                    return;
                }
            }

            var selectedRect = selectedTransform.GetComponent<RectTransform>();
            SetDesiredHeight(Mathf.Abs(selectedRect.localPosition.y) - selectedRect.sizeDelta.y * (1f - selectedRect.pivot.y));
        }

        private void SetDesiredHeight(float height)
        {
            var sizeGap = scrollView.content.sizeDelta.y - rectTransform.sizeDelta.y;
            if (sizeGap <= 0) 
                desiredPosition = 0f;
            else 
                desiredPosition = Mathf.Clamp(height, 0, sizeGap);
        }
    }
}
