using DinoMaker.Models;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace DinoMaker.UI
{
    public class OptionArea : MonoBehaviour
    {
        private readonly Dictionary<Category, List<OptionButton>> _optionButtonListLookup = new();
        
        [SerializeField] private OptionButton buttonPrefab;
        [SerializeField] private Transform buttonParent;
        [SerializeField] private TMP_InputField labelInput;

        private List<OptionButton> _activeButtons;

        public void HandleLabelEdited(string editedValue)
        {
            DinoController.Instance.SetLabelText(editedValue);
        }
        
        private void Awake()
        {
            CategoryBar.OnCategorySelected += HandleCategorySelected;
            CategoryBar.OnLabelCategorySelected += HandleLabelCategorySelected;
        }

        private void OnDestroy()
        {
            CategoryBar.OnCategorySelected -= HandleCategorySelected;
            CategoryBar.OnLabelCategorySelected -= HandleLabelCategorySelected;
        }

        private void HandleCategorySelected(Category category)
        {
            if (labelInput.gameObject.activeInHierarchy)
            {
                labelInput.gameObject.SetActive(false);
            }
            
            if (!_optionButtonListLookup.TryGetValue(category, out var buttons))
            {
                buttons = CreateButtonsForCategory(category.Options);
                _optionButtonListLookup.Add(category, buttons);
            }

            if (_activeButtons != null)
            {
                SetAreButtonsEnabled(_activeButtons, false);
            }

            _activeButtons = buttons;
            SetAreButtonsEnabled(_activeButtons, true);
        }

        private void HandleLabelCategorySelected()
        {
            if (_activeButtons != null)
            {
                SetAreButtonsEnabled(_activeButtons, false);
            }
            
            labelInput.gameObject.SetActive(true);
        }

        private static void SetAreButtonsEnabled(List<OptionButton> buttons, bool areEnabled)
        {
            foreach (OptionButton optionButton in buttons)
            {
                optionButton.gameObject.SetActive(areEnabled);
            }
        }

        private List<OptionButton> CreateButtonsForCategory(CategoryOption[] options)
        {
            List<OptionButton> newList = new List<OptionButton>();
            
            for (int i = 0, length = options.Length; i < length; i++)
            {
                newList.Add(CreateButton(options[i]));
            }

            return newList;
        }

        private OptionButton CreateButton(CategoryOption option)
        {
            OptionButton button = Instantiate(buttonPrefab, buttonParent);
            button.OnSelected += HandleButtonSelected;
            button.Configure(option);
            return button;
        }

        private static void HandleButtonSelected(OptionButton button)
        {
            DinoController.Instance.SelectOption(button);
        }
    }
}
