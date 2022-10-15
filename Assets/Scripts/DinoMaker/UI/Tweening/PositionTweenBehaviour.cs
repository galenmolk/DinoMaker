using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace DinoMaker.UI.Tweening
{
    [RequireComponent(typeof(RectTransform))]
    public class PositionTweenBehaviour : TweenBehaviour<Vector2TweenOption, Vector2, TweenerCore<Vector2, Vector2, VectorOptions>>
    {
        private RectTransform _rectTransform;

        public override void Kill()
        {
            _rectTransform.DOKill();
        }

        protected override TweenerCore<Vector2, Vector2, VectorOptions> ExecuteTween(Vector2 value, float duration)
        {
            return _rectTransform.DOAnchorPos(value, duration);
        }
        
        private void Awake()
        {
            _rectTransform = transform as RectTransform;
        }
    }
}
