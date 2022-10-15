using DG.Tweening;
using JetBrains.Annotations;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace DinoMaker.UI.Tweening
{
    [Serializable]
    public abstract class TweenOption<T>
    {
        public float Duration => duration;
        public Ease Ease => ease;
        public UnityEvent OnPreTween => onPreTween;
        public UnityEvent OnPostTween => onPostTween;
        public T Value => value;

        [Tooltip("Used in Inspector only for human recognition.")]
        [UsedImplicitly]
        [SerializeField] private string name;
        [SerializeField] private float duration;
        [SerializeField] private Ease ease;
        [SerializeField] private UnityEvent onPreTween;
        [SerializeField] private UnityEvent onPostTween;
        [SerializeField] private T value;
    }
    
    [Serializable]
    public class FloatTweenOption : TweenOption<float> { }
    
    [Serializable]
    public class Vector2TweenOption : TweenOption<Vector2> { }
}
