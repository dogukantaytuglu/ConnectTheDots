using System.Collections.Generic;
using DG.Tweening;
using Game.Features.Dot.Scripts.Settings;
using TMPro;
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
        [SerializeField] private Transform visualChildTransform;
        [SerializeField] private TextMeshPro valueText;
        
        private DotSettings _dotSettings;
        private AnimationState _currentAnimationState = AnimationState.Idle;
        private readonly List<Tween> _allTweenList = new();
        private Tween _moveToMergePositionTween;
        private Tween _scaleTween;
        private Tween _dropTween;
        private Tween _spawnAnimationTween;
        private Sequence _bounceAnimation;
        private Tween _popAnimationTween;

        [Inject]
        public void Construct(DotSettings dotSettings)
        {
            _dotSettings = dotSettings;
        }

        private void Awake()
        {
            _allTweenList.Add(_moveToMergePositionTween);
            _allTweenList.Add(_scaleTween);
            _allTweenList.Add(_bounceAnimation);
            _allTweenList.Add(_spawnAnimationTween);
            _allTweenList.Add(_popAnimationTween);
        }

        public void ScaleUp()
        {
            if (_currentAnimationState != AnimationState.Idle) return;
            KilLAllTween();
            _currentAnimationState = AnimationState.Selected;
            _scaleTween = transform.DOScale(_dotSettings.ScaleAmount ,_dotSettings.ScaleDuration);
        }

        public void ScaleDown()
        {
            if (_currentAnimationState != AnimationState.Selected) return;
            KilLAllTween();
            _currentAnimationState = AnimationState.Idle;
            _scaleTween = transform.DOScale(1f, _dotSettings.ScaleDuration);
        }

        public void MoveToMergePosition(Vector3 targetPosition, TweenCallback onCompleteAction)
        {
            KilLAllTween();
            _currentAnimationState = AnimationState.Moving;
            var duration = _dotSettings.MergeMovementDuration;
            _moveToMergePositionTween = transform.DOMove(targetPosition, duration);
            FadeTextAnimationTo(0f, duration - duration * 0.5f);

            if (onCompleteAction != null) 
                _moveToMergePositionTween.onComplete += onCompleteAction;
        }

        private void PlayBounceAnimation()
        {
            var halfTotalDuration = _dotSettings.BounceTotalDuration * 0.5f;
            _bounceAnimation = DOTween.Sequence();
            var initYScale = visualChildTransform.localScale.y;
            var initLocalYPosition = visualChildTransform.localPosition.y;

            var randomBouncePower = Random.Range(_dotSettings.MinBouncePower, _dotSettings.MaxBouncePower);
            _bounceAnimation.Append(visualChildTransform.DOScaleY(initYScale * 1 - randomBouncePower, halfTotalDuration));
            _bounceAnimation.Join(visualChildTransform.DOLocalMoveY(initLocalYPosition - randomBouncePower * 0.5f, halfTotalDuration));
            _bounceAnimation.Append(visualChildTransform.DOScaleY(initYScale, halfTotalDuration));
            _bounceAnimation.Join(visualChildTransform.DOLocalMoveY(initLocalYPosition, halfTotalDuration));
        }

        public void MoveToDropPosition(Vector3 targetPosition)
        {
            KilLAllTween();
            _currentAnimationState = AnimationState.Moving;
            _dropTween = transform.DOMove(targetPosition, _dotSettings.DropDownMovementDuration);
            _dropTween.onComplete += () => _currentAnimationState = AnimationState.Idle;
            _dropTween.onComplete += PlayBounceAnimation;
        }

        public void PlaySpawnAnimation()
        {
            FadeTextAnimationTo(0,0);
            var t = transform;
            var initScale = t.localScale;
            t.localScale = Vector3.zero;
            var duration = _dotSettings.SpawnAnimationDuration;
            _spawnAnimationTween = t.DOScale(initScale, duration);
            FadeTextAnimationTo(1, duration - duration * 0.2f);
        }

        public void PlayPopAnimation()
        {
            _popAnimationTween = transform.DOPunchScale(Vector3.one * _dotSettings.PopOnMergeStrength,
                _dotSettings.PopOnMergeDuration);
        }

        private void FadeTextAnimationTo(float value, float duration)
        {
            valueText.DOFade(value, duration);
        }

        private void OnDisable()
        {
            KilLAllTween();
        }

        private void KilLAllTween()
        {
            foreach (var tween in _allTweenList)
            {
                tween.Kill(true);
            }
        }
    }
}