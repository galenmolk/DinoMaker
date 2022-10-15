using DG.Tweening;
using UnityEngine;

namespace DinoMaker.UI.Tweening
{
    public abstract class TweenBehaviour<TTweenOption, TValue, TTween> : MonoBehaviour 
        where TTweenOption : TweenOption<TValue>
        where TTween : Tween
    {
        [SerializeField] private TTweenOption[] tweens;

        private TweenCallback _tweenCallback;

        public abstract void Kill();
        
        public void TweenToIndex(int index)
        {
            if (index < 0 || index >= tweens.Length)
            {
                Debug.LogError($"{gameObject}: Tween index {index} is invalid.");
                return;
            }

            TTweenOption tween = tweens[index];
            
            tween.OnPreTween?.Invoke();
            
            ExecuteTween(tween.Value, tween.Duration).SetEase(tween.Ease).OnComplete(() =>
            {
                tween.OnPostTween?.Invoke();
            });
        }

        protected abstract TTween ExecuteTween(TValue value, float duration);
    }
}
