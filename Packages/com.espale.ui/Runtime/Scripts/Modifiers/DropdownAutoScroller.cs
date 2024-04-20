using TMPro;
using UnityEngine;

namespace Espale.UI
{
    [RequireComponent(typeof(TMP_Dropdown))]
    public class DropdownAutoScroller : MonoBehaviour
    {
        [Header("Activation")]
        public bool isActive = true;
        
        [Tooltip("Set this to true if you want to auto scroll on expand even though isActive is set to false.")]
        public bool alwaysAutoScrollOnExpand = false;
        
        [Header("Config")]
        public bool useUnscaledTime = false;
        [Range(1f, 100f)] public float autoScrollSpeed = 10f;
       
        private bool wasExpanded;
        private TMP_Dropdown dropdown;
        
        private void Awake()
        {
            dropdown = GetComponent<TMP_Dropdown>();
            
            var scrollerTemplate =dropdown.template.gameObject.AddComponent<ScrollViewAutoScroller>();
            scrollerTemplate.updateVerticalNavOnEnable = false;
        }

        private void Update()
        {
            if (dropdown.IsExpanded && !wasExpanded)
                OnDropdownExpand();
            
            wasExpanded = dropdown.IsExpanded;
        }

        private void OnDropdownExpand()
        {
            var dropDownList = transform.Find("Dropdown List");
            if (!dropDownList) return;

            var scroller = dropDownList.GetComponent<ScrollViewAutoScroller>();
            scroller.isActive = isActive;
            scroller.autoScrollSpeed = autoScrollSpeed;
            scroller.useUnscaledTime = useUnscaledTime;
            
            scroller.UpdateChildren();

            // Force auto scroll
            if (!isActive && alwaysAutoScrollOnExpand)
            {
                scroller.ScrollToIndex(dropdown.value);
                scroller.StepScrollPosition(1f, 9999f);
            }
        }
    }
}
