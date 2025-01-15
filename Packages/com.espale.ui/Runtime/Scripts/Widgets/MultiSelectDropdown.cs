using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Linq;
using Espale.Localization;
using Espale.Utilities;
using NaughtyAttributes;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Espale.UI
{
    public class MultiSelectDropdown : Selectable, IPointerClickHandler, ISubmitHandler, ICancelHandler
    {
        protected internal class MultiSelectDropdownItem : MonoBehaviour, IPointerEnterHandler, ICancelHandler
        {
            public TMP_Text text;
            public Image image;
            public Image bg;
            public RectTransform rectTransform;
            public Toggle toggle;

            public void OnPointerEnter(PointerEventData eventData)
            {
                EventSystem.current.SetSelectedGameObject(gameObject);
            }

            public void OnCancel(BaseEventData eventData)
            {
                var dropdown = GetComponentInParent<MultiSelectDropdown>();
                if (dropdown) dropdown.Hide();
            }
        }

        [Serializable]
        public class MultiSelectDropdownOptionData
        {
            public string text;
            public Sprite image;
            public Color bgColor = Color.white;
            public Color textColor = new (.19f, .19f, .19f);
        }
        
        [Header("Options")]
        public MultiDropdownDisplayOrder displayOrder;
        public List<MultiSelectDropdownOptionData> options;
        
        [Header("Template")]
        public TMP_Text labelText;
        public Image labelImage;
        public Sprite multipleChoicesLabelImage = null;

        [Header("Template")]
        public GameObject dropdownTemplate;
        public TMP_Text itemText;
        public Image itemImage;
        public Image itemBg;
        private bool hasSetupTemplate = false;
        
        [Header("No Selection Text Config")]
        public string noSelectionText = "Select...";
        [LocalizedKey] public string noSelectionTextKey;
        
        public string selectionMergeText = "{0}, {1}";
        [LocalizedKey] public string selectionMergeTextKey;
        
        public string containsMoreText = "+{0} more";
        [LocalizedKey] public string containsMoreTextKey;

        public int maxSelectionCountToDisplayMoreText = 2;
        
        [Header("Selection")]
        public UnityEvent<List<int>> onSelectionChanged = new ();
        private List<int> selectedIndexes = new ();

        private GameObject dropdownInstance;
        private GameObject blockerInstance;
        private List<MultiSelectDropdownItem> itemInstances = new ();

        protected override void OnEnable()
        {
            base.OnEnable();
            UpdateLabelAndImage();
        }

        protected override void OnDisable()
        {
            DestroyDropdownList();

            if (blockerInstance is not null)
            {
                if (Application.isPlaying) Destroy(blockerInstance);
                else DestroyImmediate(dropdownInstance);
            }
            blockerInstance = null;

            base.OnDisable();
        }
        
        private void DestroyDropdownList()
        {
            if (dropdownInstance is not null)
            {
                if (Application.isPlaying) Destroy(dropdownInstance);
                else DestroyImmediate(dropdownInstance);
            }
            dropdownInstance = null;
            
            itemInstances.Clear();
        }
        
        private void SetupTemplate()
        {
            hasSetupTemplate = false;

            if (!dropdownTemplate)
            {
                BetterDebug.LogError("The dropdown template is not assigned. The template needs to be assigned and must have a child GameObject with a Toggle component serving as the item.", this);
                return;
            }

            dropdownTemplate.SetActive(true);
            var itemToggle = dropdownTemplate.GetComponentInChildren<Toggle>();

            hasSetupTemplate = true;
            if (!itemToggle || itemToggle.transform == dropdownTemplate.transform)
            {
                hasSetupTemplate = false;
                BetterDebug.LogError("The dropdown template is not valid. The template must have a child GameObject with a Toggle component serving as the item.", dropdownTemplate);
            }
            else if (!(itemToggle.transform.parent is RectTransform))
            {
                hasSetupTemplate = false;
                BetterDebug.LogError("The dropdown template is not valid. The child GameObject with a Toggle component (the item) must have a RectTransform on its parent.", dropdownTemplate);
            }
            else if (itemText != null && !itemText.transform.IsChildOf(itemToggle.transform))
            {
                hasSetupTemplate = false;
                BetterDebug.LogError("The dropdown template is not valid. The Item Text must be on the item GameObject or children of it.", dropdownTemplate);
            }
            else if (itemImage != null && !itemImage.transform.IsChildOf(itemToggle.transform))
            {
                hasSetupTemplate = false;
                BetterDebug.LogError("The dropdown template is not valid. The Item Image must be on the item GameObject or children of it.", dropdownTemplate);
            }

            if (!hasSetupTemplate)
            {
                dropdownTemplate.SetActive(false);
                return;
            }

            var item = itemToggle.gameObject.AddComponent<MultiSelectDropdownItem>();
            item.text = itemText;
            item.image = itemImage;
            item.toggle = itemToggle;
            item.rectTransform = (RectTransform)itemToggle.transform;
            item.bg = itemBg;

            // Find the Canvas that this dropdown is a part of
            var parentCanvas = dropdownTemplate.GetComponentInParent<Canvas>();
            var popupCanvas = GetOrAddComponent<Canvas>(dropdownTemplate);
            popupCanvas.overrideSorting = true;
            popupCanvas.sortingOrder = 30000;

            // If we have a parent canvas, apply the same raycasters as the parent for consistency.
            if (parentCanvas is not null)
            {
                foreach (var t in parentCanvas.GetComponents<BaseRaycaster>().Select(r => r.GetType()))
                    if (dropdownTemplate.GetComponent(t) is null)
                        dropdownTemplate.AddComponent(t);
            }

            GetOrAddComponent<GraphicRaycaster>(dropdownTemplate);
            GetOrAddComponent<CanvasGroup>(dropdownTemplate);
            
            dropdownTemplate.SetActive(false);
            hasSetupTemplate = true;
        }
        
        public void Show()
        {
            if (!IsActive() || !IsInteractable() || dropdownInstance != null)
                return;

            var rootCanvas = GetComponentInParent<Canvas>();
            if (rootCanvas == null)
                BetterDebug.LogError("Multi Select Dropdown object needs a parent with a canvas to work properly.", this);
            
            if (!hasSetupTemplate)
            {
                SetupTemplate();
                if (!hasSetupTemplate) return;
            }
            
            dropdownTemplate.GetComponent<Canvas>().sortingLayerID = rootCanvas.sortingLayerID;
            
            dropdownInstance = Instantiate(dropdownTemplate, dropdownTemplate.transform.parent, false);
            dropdownInstance.SetActive(true);
            var dropdownRectTransform = dropdownInstance.transform as RectTransform;
            
            // Find the dropdown item and disable it.
            var itemTemplate = dropdownInstance.GetComponentInChildren<MultiSelectDropdownItem>();

            var content = itemTemplate.rectTransform.parent.gameObject;
            var contentRectTransform = content.transform as RectTransform;
            itemTemplate.rectTransform.gameObject.SetActive(true);
            
            // Get the rects of the dropdown and item
            var dropdownContentRect = contentRectTransform.rect;
            var itemTemplateRect = itemTemplate.rectTransform.rect;

            // Calculate the visual offset between the item's edges and the background's edges
            var offsetMin = itemTemplateRect.min - dropdownContentRect.min + (Vector2)itemTemplate.rectTransform.localPosition;
            var offsetMax = itemTemplateRect.max - dropdownContentRect.max + (Vector2)itemTemplate.rectTransform.localPosition;
            var itemSize = itemTemplateRect.size;
            
            itemInstances.Clear();

            Toggle prev = null;
            foreach (var i in GetOptionsTraversalOrder())
            {
                var data = options[i];
                var isOn = GetSelectedIndexes().Contains(i);
                var item = AddItem(data, itemTemplate, itemInstances);
                if (item == null)
                    continue;

                // Automatically set up a toggle state change listener
                item.toggle.isOn = isOn;
                var index = i;
                item.toggle.onToggle.AddListener(() =>
                {
                    if (selectedIndexes.Contains(index)) selectedIndexes.Remove(index);
                    else selectedIndexes.Add(index);
                    
                    selectedIndexes.Sort();
                    UpdateLabelAndImage();
                });

                // Select current option
                if (item.toggle.isOn)
                    item.toggle.Select();

                // Automatically set up explicit navigation
                if (prev != null)
                {
                    var prevNav = prev.navigation;
                    var toggleNav = item.toggle.navigation;
                    prevNav.mode = Navigation.Mode.Explicit;
                    toggleNav.mode = Navigation.Mode.Explicit;

                    prevNav.selectOnDown = item.toggle;
                    prevNav.selectOnRight = item.toggle;
                    toggleNav.selectOnLeft = prev;
                    toggleNav.selectOnUp = prev;

                    prev.navigation = prevNav;
                    item.toggle.navigation = toggleNav;
                }
                prev = item.toggle;
            }
            
            // Reposition all items now that all of them have been added
            var sizeDelta = contentRectTransform.sizeDelta;
            sizeDelta.y = itemSize.y * itemInstances.Count + offsetMin.y - offsetMax.y;
            contentRectTransform.sizeDelta = sizeDelta;

            var extraSpace = dropdownRectTransform.rect.height - contentRectTransform.rect.height;
            if (extraSpace > 0)
                dropdownRectTransform.sizeDelta = new Vector2(dropdownRectTransform.sizeDelta.x, dropdownRectTransform.sizeDelta.y - extraSpace);

            // Invert anchoring and position if dropdown is partially or fully outside of canvas rect.
            // Typically this will have the effect of placing the dropdown above the button instead of below,
            // but it works as inversion regardless of initial setup.
            var corners = new Vector3[4];
            dropdownRectTransform.GetWorldCorners(corners);

            var rootCanvasRectTransform = rootCanvas.transform as RectTransform;
            var rootCanvasRect = rootCanvasRectTransform.rect;
            for (var axis = 0; axis < 2; axis++)
            {
                var outside = false;
                for (var i = 0; i < 4; i++)
                {
                    var corner = rootCanvasRectTransform.InverseTransformPoint(corners[i]);
                    if ((corner[axis] < rootCanvasRect.min[axis] && !Mathf.Approximately(corner[axis], rootCanvasRect.min[axis])) ||
                        (corner[axis] > rootCanvasRect.max[axis] && !Mathf.Approximately(corner[axis], rootCanvasRect.max[axis])))
                    {
                        outside = true;
                        break;
                    }
                }
                if (outside)
                    RectTransformUtility.FlipLayoutOnAxis(dropdownRectTransform, axis, false, false);
            }

            for (int i = 0; i < itemInstances.Count; i++)
            {
                RectTransform itemRect = itemInstances[i].rectTransform;
                itemRect.anchorMin = new Vector2(itemRect.anchorMin.x, 0);
                itemRect.anchorMax = new Vector2(itemRect.anchorMax.x, 0);
                itemRect.anchoredPosition = new Vector2(itemRect.anchoredPosition.x, offsetMin.y + itemSize.y * (itemInstances.Count - 1 - i) + itemSize.y * itemRect.pivot.y);
                itemRect.sizeDelta = new Vector2(itemRect.sizeDelta.x, itemSize.y);
            }

            // Make drop-down template and item template inactive
            dropdownTemplate.gameObject.SetActive(false);
            itemTemplate.gameObject.SetActive(false);

            blockerInstance = CreateBlocker(rootCanvas);
        }

        private int[] GetOptionsTraversalOrder()
        {
            if (displayOrder is MultiDropdownDisplayOrder.SelectionsFirst)
            {
                // Make the traversal order, start from the selected items, then, the rest of the items.
                var traversalOrder = new int[options.Count];
                for (var i = 0; i < options.Count; i++) traversalOrder[i] = -1;
            
                for (var i = 0; i < selectedIndexes.Count; i++)
                    traversalOrder[i] = selectedIndexes[i];

                var additionCount = 0;
                for (var i = 0; i < options.Count; i++)
                {
                    if (selectedIndexes.Contains(i)) continue;
                    
                    traversalOrder[additionCount + selectedIndexes.Count] = i;
                    additionCount++;
                }
            
                return traversalOrder;
            }
            if (displayOrder is MultiDropdownDisplayOrder.SelectionsLast)
            {
                // Make the traversal order, start from the selected items, then, the rest of the items.
                var traversalOrder = new int[options.Count];
                for (var i = 0; i < options.Count; i++) traversalOrder[i] = -1;
            

                var additionCount = 0;
                for (var i = 0; i < options.Count; i++)
                {
                    if (selectedIndexes.Contains(i)) continue;
                    
                    traversalOrder[additionCount] = i;
                    additionCount++;
                }
                
                for (var i = 0; i < selectedIndexes.Count; i++)
                    traversalOrder[additionCount + i] = selectedIndexes[i];
            
                return traversalOrder;
            }
            
            // Default Order
            var defaultTraversalOrder = new int[options.Count];
            for (var i = 0; i < options.Count; i++) defaultTraversalOrder[i] = i;
            return defaultTraversalOrder;
        }
        
        private GameObject CreateBlocker(Canvas rootCanvas)
        {
            // Create blocker GameObject.
            var blocker = new GameObject("Blocker");

            // Setup blocker RectTransform to cover entire root canvas area.
            var blockerRect = blocker.AddComponent<RectTransform>();
            blockerRect.SetParent(rootCanvas.transform, false);
            blockerRect.anchorMin = Vector3.zero;
            blockerRect.anchorMax = Vector3.one;
            blockerRect.sizeDelta = Vector2.zero;

            // Make blocker be in separate canvas in same layer as dropdown and in layer just below it.
            var blockerCanvas = blocker.AddComponent<Canvas>();
            blockerCanvas.overrideSorting = true;
            
            var dropdownCanvas = dropdownInstance.GetComponent<Canvas>();
            blockerCanvas.sortingLayerID = dropdownCanvas.sortingLayerID;
            blockerCanvas.sortingOrder = dropdownCanvas.sortingOrder - 1;

            // Find the Canvas that this dropdown is a part of
            Canvas parentCanvas = null;
            var parentTransform = dropdownTemplate.transform.parent;
            while (parentTransform != null)
            {
                parentCanvas = parentTransform.GetComponent<Canvas>();
                if (parentCanvas != null)
                    break;

                parentTransform = parentTransform.parent;
            }

            // If we have a parent canvas, apply the same raycasters as the parent for consistency.
            if (parentCanvas is not null)
            {
                foreach (var t in parentCanvas.GetComponents<BaseRaycaster>().Select(c => c.GetType()))
                    if (blocker.GetComponent(t) is null)
                        blocker.AddComponent(t);
            }
            else
                // Add raycaster since it's needed to block.
                GetOrAddComponent<GraphicRaycaster>(blocker);


            // Add image since it's needed to block, but make it clear.
            var blockerImage = blocker.AddComponent<Image>();
            blockerImage.color = Color.clear;

            // Add button since it's needed to block, and to close the dropdown when blocking area is clicked.
            var blockerButton = blocker.AddComponent<Button>();
            blockerButton.onClick.AddListener(Hide);

            return blocker;
        }
        
        // Add a new drop-down list item with the specified values.
        private MultiSelectDropdownItem AddItem(MultiSelectDropdownOptionData data, MultiSelectDropdownItem itemTemplate, List<MultiSelectDropdownItem> items)
        {
            // Add a new item to the dropdown.
            var item = Instantiate(itemTemplate, itemTemplate.rectTransform.parent, false);

            item.gameObject.SetActive(true);
            item.gameObject.name = "Item " + items.Count + (data.text != null ? ": " + data.text : "");

            if (item.toggle) item.toggle.isOn = false;

            // Set the item's data
            if (item.text) item.text.text = data.text;
            if (item.image)
            {
                item.image.sprite = data.image;
                item.image.enabled = item.image.sprite;
            }

            if (item.bg) item.bg.color = data.bgColor;
            item.text.color = data.textColor;

            items.Add(item);
            return item;
        }

        public void Hide()
        {
            if (dropdownInstance is not null)
                DestroyDropdownList();

            if (blockerInstance) Destroy(blockerInstance);
            blockerInstance = null;
            
            Select();
        }

        private void UpdateToggles()
        {
            if (itemInstances is null) return;

            for (var i = 0; i < itemInstances.Count; i++)
                itemInstances[i].toggle.SetIsOnStateInstantly(selectedIndexes.Contains(i));
        }

        private void UpdateLabelAndImage()
        {
            if (selectedIndexes.Count == 0) labelText.text = string.IsNullOrWhiteSpace(noSelectionTextKey) ? noSelectionText : Localizator.GetString(noSelectionTextKey);
            else
            {
                var selections = GetSelectedOptionLabels();
                var text = selections[0];
                for (var i = 1; i < selections.Count; i++)
                {
                    var mergeT = string.IsNullOrWhiteSpace(selectionMergeTextKey) ? selectionMergeText : Localizator.GetString(selectionMergeTextKey);
                    if (i > maxSelectionCountToDisplayMoreText - 1 && maxSelectionCountToDisplayMoreText >= 1)
                    {
                        var containsMoreT = string.IsNullOrWhiteSpace(containsMoreTextKey) ? containsMoreText : Localizator.GetString(containsMoreTextKey);
                        text = string.Format(mergeT, text, string.Format(containsMoreT, selections.Count - maxSelectionCountToDisplayMoreText));
                        break;
                    }
                    text = string.Format(mergeT, text, selections[i]);
                }

                labelText.text = text;
            }
            
            if (selectedIndexes.Count == 1)
                labelImage.sprite = GetSelectedOptionImages()[0]; 
            else if (selectedIndexes.Count > 1)
                labelImage.sprite = multipleChoicesLabelImage ? multipleChoicesLabelImage : GetSelectedOptionImages()[1];
            else
                labelImage.sprite = null;
            
            labelImage.enabled = selectedIndexes.Count > 0 && labelImage.sprite;
        }

        public void SetSelections(List<int> indices)
        {
            selectedIndexes = new List<int>(indices);
            UpdateLabelAndImage();
            onSelectionChanged.Invoke(selectedIndexes);
            
            if (dropdownInstance != null) UpdateToggles();
        }

        public List<int> GetSelectedIndexes() => new (selectedIndexes);
        public List<string> GetSelectedOptionLabels()
            => selectedIndexes
                .OrderBy(i => i)
                .Select(index => options[index].text)
                .ToList();
        public List<Sprite> GetSelectedOptionImages()
            => selectedIndexes
                .OrderBy(i => i)
                .Select(index => options[index].image)
                .ToList();

        public void ClearSelections()
        {
            selectedIndexes.Clear();
            UpdateLabelAndImage();
            onSelectionChanged.Invoke(selectedIndexes);
            
            if (dropdownInstance != null) UpdateToggles();
        }
        
        public virtual void OnPointerClick(PointerEventData eventData) => Show();
        public virtual void OnSubmit(BaseEventData eventData) => Show();
        public virtual void OnCancel(BaseEventData eventData) => Hide();
        
        private static T GetOrAddComponent<T>(GameObject go) where T : Component
        {
            T comp = go.GetComponent<T>();
            if (!comp)
                comp = go.AddComponent<T>();
            return comp;
        }
        
        public enum MultiDropdownDisplayOrder
        {
            ConstantOrder = 0,
            SelectionsFirst = 1,
            SelectionsLast = 2,
        }
    }
}
