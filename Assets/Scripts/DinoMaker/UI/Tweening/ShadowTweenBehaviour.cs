using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.UI;

namespace DinoMaker.UI.Tweening
{
    [RequireComponent(typeof(DropShadow))]
    public class ShadowTweenBehaviour : TweenBehaviour<FloatTweenOption, float, TweenerCore<Color, Color, ColorOptions>>
    {
        private DropShadow _dropShadow;

        public override void Kill()
        {
            _dropShadow.DOKill();
        }

        protected override TweenerCore<Color, Color, ColorOptions> ExecuteTween(float value, float duration)
        {
            return _dropShadow.DOFade(value, duration);
        }
        
        private void Awake()
        {
            _dropShadow = GetComponent<DropShadow>();
        }
    }
}
