using DinoMaker.UI.Tweening;
using DinoMaker.Utils;
using MolkExtras;
using TMPro;
using UnityEngine;

namespace DinoMaker.UI
{
    public class CategoryToolTip : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;
        [SerializeField] private CanvasGroupTweenBehaviour canvasGroupTweenBehaviour;
        
        private void Awake()
        {
            CategoryButton.OnHoverStart += HandleHoverStart;
            CategoryButton.OnHoverEnd += HandleHoverEnd;
        }

        private void OnDestroy()
        {
            CategoryButton.OnHoverStart -= HandleHoverStart;
            CategoryButton.OnHoverEnd -= HandleHoverEnd;
        }

        private void HandleHoverStart(CategoryButton button)
        {
            text.text = button.Category.CategoryName;

            if (!canvasGroupTweenBehaviour.CanvasGroup.IsMaxAlpha())
            {
                canvasGroupTweenBehaviour.TweenToIndex(ProjectConsts.OPEN_TWEEN_INDEX);
            }
        }

        private void HandleHoverEnd()
        {
            canvasGroupTweenBehaviour.TweenToIndex(ProjectConsts.CLOSE_TWEEN_INDEX);
        }
    }
}
