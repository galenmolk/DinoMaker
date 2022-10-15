using DG.Tweening;
using DinoMaker.Models;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace DinoMaker.UI
{
    public class OptionButton : MonoBehaviour
    {
        public event Action<OptionButton> OnSelected;
        
        public CategoryOption Option { get; private set; }

        private readonly Vector3 _selectedRotation = new(0f, 0f, 10f);
        private readonly Vector3 _defaultRotation = Vector3.zero;
        
        [SerializeField] private Image icon;
        [SerializeField] private GameObject border;
        [SerializeField] private float rotationTweenDuration = 0.2f;
        
        private Button _button;
        private RectTransform _rectTransform;
        
        public void Configure(CategoryOption asset)
        {
            Option = asset;
            icon.sprite = Option.Sprite;
            icon.rectTransform.anchoredPosition = Option.IconOffset;
            icon.rectTransform.localScale = Option.IconScale;
            icon.color = Option.Color;
            gameObject.name = asset.name;
            
            if (asset.IsDefaultOption)
            {
                HandleClick();
            }
        }

        public void SetIsSelected(bool isSelected)
        {
            _rectTransform.DORotateQuaternion(Quaternion.Euler(isSelected ? _selectedRotation : _defaultRotation), rotationTweenDuration);
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
    }
}
