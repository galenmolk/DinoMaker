using DinoMaker.Utils;
using UnityEngine;

namespace DinoMaker.Models
{
    [CreateAssetMenu(fileName = NAME, menuName = ProjectConsts.CUSTOM_ASSET_MENU + NAME, order = 0)]
    public class CategoryOption : ScriptableObject
    {
        private const string NAME = nameof(CategoryOption);

        public Sprite Sprite => sprite;
        public Vector2 IconOffset => iconOffset;
        public Color Color => color;

        public bool IsDefaultOption => isDefaultOption;

        public Vector3 IconScale => iconScale;

        [SerializeField] private bool isDefaultOption;
        
        [SerializeField] private Sprite sprite;
        [SerializeField] private Color color = Color.white;
        [Tooltip("Y offset applied to the anchored position of this options button so the image is visible.")]
        [SerializeField] private Vector2 iconOffset;
        [SerializeField] private Vector3 iconScale = Vector3.one;
    }
}
