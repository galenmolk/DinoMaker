using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace DinoMaker.UI.Tweening
{
    public class ScaleTweenBehaviour : TweenBehaviour<FloatTweenOption, float, TweenerCore<Vector3, Vector3, VectorOptions>>
    {
        private RectTransform _rectTransform;

        public override void Kill()
        {
            _rectTransform.DOKill();
        }

        protected override TweenerCore<Vector3, Vector3, VectorOptions> ExecuteTween(float value, float duration)
        {
            return _rectTransform.DOScale(value, duration);
        }
        
        private void Awake()
        {
            _rectTransform = transform as RectTransform;
        }
    }
}
