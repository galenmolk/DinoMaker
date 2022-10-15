using DinoMaker.Models;
using DinoMaker.UI;
using DinoMaker.UI.Tweening;
using MolkExtras;
using System.Collections.Generic;
using DinoMaker.Utils;
using TMPro;
using UnityEngine;

namespace DinoMaker
{
    public class DinoController : Singleton<DinoController>
    {
        public RectTransform DinoBody => dinoBody;
        
        [SerializeField] private RectTransform dinoBody;
        [SerializeField] private ScaleTweenBehaviour scaleTweenBehaviour;

        [SerializeField] private TMP_Text labelSection;
        
        private readonly Dictionary<Category, DinoSection> _sectionLookUp = new();

        private CategoryButton _selectedCategoryButton;
        private DinoSection _selectedSection;
        private DinoSection[] _sections;
        
        public void SetLabelText(string text)
        {
            labelSection.text = text;
        }
        
        public void SelectOption(OptionButton optionButton)
        {
            _selectedCategoryButton.UpdateThumbnail(optionButton.Option);
            _selectedSection.AssignOption(optionButton);
        }

        public void SelectCategory(CategoryButton categoryButton)
        {
            if (_selectedCategoryButton != null)
            {
                _selectedCategoryButton.SetIsSelected(false);
            }
            
            scaleTweenBehaviour.TweenToIndex(ProjectConsts.OPEN_TWEEN_INDEX);
            _selectedCategoryButton = categoryButton;
            _selectedCategoryButton.SetIsSelected(true);
            _selectedSection = GetDinoSectionForCategory(_selectedCategoryButton.Category);
        }

        public void SelectLabelCategory(CategoryButton labelCategoryButton)
        {
            if (_selectedCategoryButton != null)
            {
                _selectedCategoryButton.SetIsSelected(false);
            }
            
            scaleTweenBehaviour.TweenToIndex(ProjectConsts.OPEN_TWEEN_INDEX);
            _selectedCategoryButton = labelCategoryButton;
            _selectedCategoryButton.SetIsSelected(true);
        }

        public void CloseCategory()
        {
            scaleTweenBehaviour.TweenToIndex(ProjectConsts.CLOSE_TWEEN_INDEX);
            _selectedCategoryButton.SetIsSelected(false);
            _selectedCategoryButton = null;
        }

        protected override void OnAwake()
        {
            base.OnAwake();
            _sections = GetComponentsInChildren<DinoSection>();
        }

        private DinoSection GetDinoSectionForCategory(Category category)
        {
            if (_sectionLookUp.TryGetValue(category, out DinoSection section))
            {
                return section;
            }

            section = FindSection(category);
            _sectionLookUp.Add(category, section);
            return section;
        }

        private DinoSection FindSection(Object category)
        {
            for (int i = 0, length = _sections.Length; i < length; i++)
            {
                DinoSection section = _sections[i];
                
                if (section.Category == category)
                {
                    return section;
                }
            }
            
            Debug.LogError($"No {nameof(DinoSection)} found for Category {category.name}");
            return null;
        }
    }
}
