using System;
using DinoMaker.Utils;
using UnityEditor;
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

        // CURSED CODE
        
        // private void OnValidate()
        // {
        //     if (sprite == null)
        //     {
        //         return;
        //     }
        //
        //     Sprite mySprite = sprite;
        //     
        //     string spriteName = sprite.name;
        //     string currentName = name;
        //
        //     if (string.Equals(spriteName, currentName))
        //     {
        //         return;
        //     }
        //
        //     string currentPath = AssetDatabase.GetAssetPath(this);
        //     string newPath = currentPath.Replace(currentName, spriteName);
        //
        //     if (!string.IsNullOrEmpty(AssetDatabase.AssetPathToGUID(newPath)))
        //     {
        //         return;
        //     }
        //     
        //     string errorMessage = AssetDatabase.RenameAsset(currentPath, spriteName);
        //     
        //     if (!string.IsNullOrWhiteSpace(errorMessage))
        //     {
        //         Debug.LogError(errorMessage);
        //     }
        //
        //     sprite = mySprite;
        // }
    }
}
