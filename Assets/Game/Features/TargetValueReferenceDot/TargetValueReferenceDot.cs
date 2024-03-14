using Game.Features.Dot.Scripts.Dot;
using Game.Features.Dot.Scripts.Signals;
using Game.Features.Dot.Scripts.Systems;
using Game.Features.Input.Scripts.Signals;
using UnityEngine;
using Zenject;

namespace Game.Features.TargetValueReferenceDot
{
    public class TargetValueReferenceDot : MonoBehaviour
    {
        private DotVisualHandler _dotVisualHandler;
        private SignalBus _signalBus;
        private DotSelectionHandler _dotSelectionHandler;

        [Inject]
        public void Construct(DotVisualHandler dotVisualHandler, SignalBus signalBus, DotSelectionHandler dotSelectionHandler)
        {
            _dotVisualHandler = dotVisualHandler;
            _signalBus = signalBus;
            _dotSelectionHandler = dotSelectionHandler;
        }
        private void Awake()
        {
            DisableDotVisual();
        }

        private void OnEnable()
        {
            _signalBus.Subscribe<InputFingerUpSignal>(DisableDotVisual);
            _signalBus.Subscribe<FirstDotSelectedSignal>(EnableDotVisualWithValue);
            _signalBus.Subscribe<SelectedDotListChangedSignal>(ChangeVisualValue);
        }

        private void ChangeVisualValue(SelectedDotListChangedSignal signal)
        {
            _dotVisualHandler.HandleVisualByValue(_dotSelectionHandler.CalculateTotalMergeValue());
        }

        private void EnableDotVisualWithValue(FirstDotSelectedSignal signal)
        {
            _dotVisualHandler.gameObject.SetActive(true);
            _dotVisualHandler.HandleVisualByValue(signal.Value);
        }

        private void DisableDotVisual()
        {
            _dotVisualHandler.gameObject.SetActive(false);
        }

        private void OnDisable()
        {
            _signalBus.Unsubscribe<InputFingerUpSignal>(DisableDotVisual);
            _signalBus.Unsubscribe<FirstDotSelectedSignal>(EnableDotVisualWithValue);
            _signalBus.Unsubscribe<SelectedDotListChangedSignal>(ChangeVisualValue);
        }
    }
}
