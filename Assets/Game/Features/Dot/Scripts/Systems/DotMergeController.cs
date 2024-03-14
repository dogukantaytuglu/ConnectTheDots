using System;
using DG.Tweening;
using Game.Features.Dot.Scripts.Dot;
using Game.Features.Dot.Scripts.Settings;
using Game.Features.Dot.Scripts.Signals;
using Zenject;

namespace Game.Features.Dot.Scripts.Systems
{
    public class DotMergeController : IInitializable, IDisposable
    {
        private readonly SignalBus _signalBus;
        private readonly DotSettings _dotSettings;
        private DotEntity[] _selectedDotEntities;
        private DotEntity _dotEntityToMerge;
        private readonly DotSelectionHandler _dotSelectionHandler;

        public DotMergeController(SignalBus signalBus, DotSettings dotSettings, DotSelectionHandler dotSelectionHandler)
        {
            _signalBus = signalBus;
            _dotSettings = dotSettings;
            _dotSelectionHandler = dotSelectionHandler;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<SelectedDotListChangedSignal>(SaveSelectedDots);
            _signalBus.Subscribe<SelectedDotsListClearedSignal>(TryMerge);
        }

        private void TryMerge()
        {
            if (IsMergePossible())
            {
                MergeAllDotsToLastDot();
                DOVirtual.DelayedCall(_dotSettings.MergeMovementDuration, FireMergeCompleteSignal);
            }
            
            else
            {
                FireMergeCompleteSignal();
            }
        }

        private bool IsMergePossible()
        {
            return _selectedDotEntities is { Length: > 1 };
        }

        private void MergeAllDotsToLastDot()
        {
            var lastSelectedDotEntity = _selectedDotEntities[^1];
            _dotEntityToMerge = lastSelectedDotEntity;
            lastSelectedDotEntity.SetValue(_dotSelectionHandler.CalculateTotalMergeValue());
            for (var i = 0; i < _selectedDotEntities.Length - 1; i++)
            {
                _selectedDotEntities[i].MergeTo(lastSelectedDotEntity);
            }
        }

        private void FireMergeCompleteSignal()
        {
            _signalBus.Fire<MergeCompleteSignal>();
            TryPopLastDotEntity();
            _selectedDotEntities = null;
        }

        private void TryPopLastDotEntity()
        {
            if (!_dotEntityToMerge) return;
            _dotEntityToMerge.PlayPopAnimation();
            _dotEntityToMerge = null;
        }

        private void SaveSelectedDots(SelectedDotListChangedSignal signal)
        {
            _selectedDotEntities = new DotEntity[signal.DotEntities.Length];
            Array.Copy(signal.DotEntities, _selectedDotEntities, signal.DotEntities.Length);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<SelectedDotListChangedSignal>(SaveSelectedDots);
            _signalBus.Unsubscribe<SelectedDotsListClearedSignal>(TryMerge);
        }
    }
}