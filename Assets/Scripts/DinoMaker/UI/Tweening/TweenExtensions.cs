using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.UI;

namespace DinoMaker.UI.Tweening
{
    public static class TweenExtensions
    {
        public static TweenerCore<Color, Color, ColorOptions> DOFade(this DropShadow target, float endValue, float duration)
        {
            TweenerCore<Color, Color, ColorOptions> t = DOTween.ToAlpha(
                () => target.EffectColor, 
                x => target.EffectColor = x, endValue, duration);
            
            t.SetTarget(target);
            return t;
        }
    }
}
