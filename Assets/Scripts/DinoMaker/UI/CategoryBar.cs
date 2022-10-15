using DinoMaker.Models;
using DinoMaker.UI.Tweening;
using System;
using DinoMaker.Utils;
using UnityEngine;

namespace DinoMaker.UI
{
    public class CategoryBar : MonoBehaviour
    {
        public static event Action<Category> OnCategorySelected; 
        public static event Action OnLabelCategorySelected; 

        [SerializeField] private Category[] categories;

        [SerializeField] private PositionTweenBehaviour positionTweenBehaviour;
        [SerializeField] private ShadowTweenBehaviour shadowTweenBehaviour;
        [SerializeField] private PositionTweenBehaviour collapseButtonTween;

        [SerializeField] private CategoryButton buttonPrefab;
        [SerializeField] private Transform buttonParent;

        [SerializeField] private CategoryButton labelCategoryPrefab;

        private CategoryButton _labelButton;
        
        private void Awake()
        {
            CreateButtons(categories);
        }

        private void CreateButtons(Category[] assets)
        {
            for (int i = 0, length = assets.Length; i < length; i++)
            {
                // Create the label category button so it's placed second-to-last.
                if (i == length - 1)
                {
                    _labelButton = Instantiate(labelCategoryPrefab, buttonParent);
                    _labelButton.OnSelected += HandleLabelCategorySelected;
                }
                
                CreateButtonInContainer(assets[i]);
            }
        }
        
        private void CreateButtonInContainer(Category category)
        {
            CategoryButton button = Instantiate(buttonPrefab, buttonParent);
            button.Configure(category);
            button.OnSelected += HandleCategorySelected;
        }

        private void HandleCategorySelected(CategoryButton button)
        {
            Open();
            DinoController.Instance.SelectCategory(button);
            OnCategorySelected?.Invoke(button.Category);
        }

        private void HandleLabelCategorySelected(CategoryButton button)
        {
            Open();
            DinoController.Instance.SelectLabelCategory(button);
            OnLabelCategorySelected?.Invoke();
        }

        private void Open()
        {
            ToggleVisuals(true);
        }

        public void CollapseOptions()
        {
            DinoController.Instance.CloseCategory();
            ToggleVisuals(false);
        }

        private void ToggleVisuals(bool isOpen)
        {
            int index = isOpen ? ProjectConsts.OPEN_TWEEN_INDEX : ProjectConsts.CLOSE_TWEEN_INDEX;
            positionTweenBehaviour.TweenToIndex(index);
            shadowTweenBehaviour.TweenToIndex(index);
            collapseButtonTween.TweenToIndex(index);
        }
    }
}
