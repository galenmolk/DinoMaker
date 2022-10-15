using DinoMaker.Models;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace DinoMaker.UI
{
    [RequireComponent(typeof(Button), typeof(RectTransform))]
    public class CategoryButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public static event Action<CategoryButton> OnHoverStart; 
        public static event Action OnHoverEnd; 
        public event Action<CategoryButton> OnSelected;

        public Category Category
        {
            get => category;
            private set => category = value;
        }

        private readonly Vector3 _selectedRotation = new(0f, 0f, 10f);
        private readonly Vector3 _defaultRotation = Vector3.zero;
        
        [SerializeField] private Image icon;
        [SerializeField] private GameObject border;
        [SerializeField] private bool rotateButton;
        [SerializeField] private Category category;
        
        private Button _button;
        private RectTransform _rectTransform;

        public void Configure(Category asset)
        {
            Category = asset;
            string assetName = asset.name;
            gameObject.name = assetName;

            if (asset.ThumbnailOption != null)
            {
                UpdateThumbnail(asset.ThumbnailOption);
            }
            else
            {
                Debug.LogWarning($"{assetName} is missing a thumbnail option.");
            }
        }

        public void UpdateThumbnail(CategoryOption option)
        {
            icon.sprite = option.Sprite;
            icon.rectTransform.anchoredPosition = option.IconOffset;
            icon.color = option.Color;
            icon.rectTransform.localScale = option.IconScale;
        }

        public void SetIsSelected(bool isSelected)
        {
            if (rotateButton)
            {
                _rectTransform.rotation = Quaternion.Euler(isSelected ? _selectedRotation : _defaultRotation);
            }
            
            border.SetActive(isSelected);
        }
        
        private void Awake()
        {
            _rectTransform = transform as RectTransform;
            _button = GetComponent<Button>();
            _button.onClick.AddListener(HandleClick);
        }
        
        private void OnDestroy()
        {
            OnSelected = null;
            _button.onClick.RemoveListener(HandleClick);
        }

        private void HandleClick()
        {
            OnSelected?.Invoke(this);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            OnHoverStart?.Invoke(this);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            OnHoverEnd?.Invoke();
        }
    }
}
