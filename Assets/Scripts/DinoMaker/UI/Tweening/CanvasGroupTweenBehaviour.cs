using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace DinoMaker.UI.Tweening
{
    [RequireComponent(typeof(CanvasGroup))]
    public class CanvasGroupTweenBehaviour : TweenBehaviour<FloatTweenOption, float, TweenerCore<float, float, FloatOptions>>
    {
        public CanvasGroup CanvasGroup { get; private set; }

        public override void Kill()
        {
            CanvasGroup.DOKill();
        }

        protected override TweenerCore<float, float, FloatOptions> ExecuteTween(float value, float duration)
        {
            return CanvasGroup.DOFade(value, duration);
        }
        
        private void Awake()
        {
            CanvasGroup = GetComponent<CanvasGroup>();
        }
    }
}
