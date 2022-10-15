using DinoMaker.UI.Tweening;
using DinoMaker.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace DinoMaker
{
    public class ResetConfirmWindow : MonoBehaviour
    {
        [SerializeField] private PositionTweenBehaviour positionTweenBehaviour;
        
        private void Awake()
        {
            SetIsActive(false);
        }

        private void SetIsActive(bool isActive)
        {
            positionTweenBehaviour.Kill();
            int index = isActive ? ProjectConsts.OPEN_TWEEN_INDEX : ProjectConsts.CLOSE_TWEEN_INDEX;
            positionTweenBehaviour.TweenToIndex(index);
        }
                
        public void TryClearOptions()
        {
            SetIsActive(true);
        }
        
        public void ConfirmResetChoices()
        {
            // Keep It Simple Stupid -- reload the scene if the clear button is pressed.
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void CancelResetChoices()
        {
            SetIsActive(false);
        }
    }
}
