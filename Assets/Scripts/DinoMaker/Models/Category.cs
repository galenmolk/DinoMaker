using DinoMaker.Utils;
using UnityEngine;

namespace DinoMaker.Models
{
    [CreateAssetMenu(fileName = NAME, menuName = ProjectConsts.CUSTOM_ASSET_MENU + NAME, order = 0)]
    public class Category : ScriptableObject
    {
        private const string NAME = nameof(Category);

        public CategoryOption ThumbnailOption => thumbnailOption;
        
        [SerializeField] private CategoryOption thumbnailOption;

        public CategoryOption[] Options => options;
        [SerializeField] private CategoryOption[] options;

        public bool Required => required;
        [Tooltip("If checked, this category cannot be removed from the Dino.")]
        [SerializeField] private bool required;

        public string CategoryName => categoryName;
        [SerializeField] private string categoryName;
    }
}
