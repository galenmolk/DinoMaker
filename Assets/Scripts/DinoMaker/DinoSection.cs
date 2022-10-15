using DinoMaker.Models;
using DinoMaker.UI;
using UnityEngine;
using UnityEngine.UI;

namespace DinoMaker
{
    public class DinoSection : MonoBehaviour
    {
        public Category Category => category;
        
        [SerializeField] private Category category;

        private Image _image;
        private OptionButton _optionButton;
        
        public void AssignOption(OptionButton optionButton)
        {
            if (_optionButton == optionButton && !category.Required)
            {
                ClearSection();
                return;
            }
            
            if (_optionButton != null)
            {
                _optionButton.SetIsSelected(false);
            }
            
            _optionButton = optionButton;
            _optionButton.SetIsSelected(true);
            _image.sprite = optionButton.Option.Sprite;
            _image.color = optionButton.Option.Color;
            _image.enabled = true;
        }

        private void ClearSection()
        {
            _optionButton.SetIsSelected(false);
            _optionButton = null;
            _image.sprite = null;
            _image.color = Color.white;
            _image.enabled = false;
        }
        
        private void Awake()
        {
            _image = GetComponent<Image>();
        }
    }
}
