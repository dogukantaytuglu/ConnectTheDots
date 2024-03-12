using DG.Tweening;
using Game.Features.Dot.Scripts.Settings;
using UnityEngine;
using Zenject;

namespace Game.Features.Dot.Scripts.Dot
{
    public enum AnimationState
    {
        Idle,
        Selected,
        Moving
    }
    public class DotAnimationHandler : MonoBehaviour
    {
        private DotSettings _dotSettings;
        private AnimationState _currentAnimationState = AnimationState.Idle;

        [Inject]
        public void Construct(DotSettings dotSettings)
        {
            _dotSettings = dotSettings;
        }
        
        public void ScaleUp()
        {
            if (_currentAnimationState != AnimationState.Idle) return;
            _currentAnimationState = AnimationState.Selected;
            transform.DOScale(_dotSettings.ScaleAmount ,_dotSettings.ScaleDuration);
        }

        public void ScaleDown()
        {
            if (_currentAnimationState != AnimationState.Selected) return;
            _currentAnimationState = AnimationState.Idle;
            transform.DOScale(1f, _dotSettings.ScaleDuration);
        }

        public Tween MoveToPosition(Vector3 targetPosition)
        {
            ScaleDown();
            _currentAnimationState = AnimationState.Moving;
            return transform.DOMove(targetPosition, _dotSettings.MergeMovementDuration);
        }
    }
}