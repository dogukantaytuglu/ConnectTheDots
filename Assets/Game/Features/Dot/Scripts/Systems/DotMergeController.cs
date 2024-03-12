using System;
using System.Linq;
using DG.Tweening;
using Game.Features.Dot.Scripts.Dot;
using Game.Features.Dot.Scripts.Settings;
using Game.Features.Dot.Scripts.Signals;
using Game.Features.Input.Scripts.Signals;
using Zenject;

namespace Game.Features.Dot.Scripts.Systems
{
    public class DotMergeController : IInitializable, IDisposable
    {
        private readonly SignalBus _signalBus;
        private readonly DotSettings _dotSettings;
        private DotEntity[] _selectedDotEntities;
        private int _baseValue;

        public DotMergeController(SignalBus signalBus, DotSettings dotSettings)
        {
            _signalBus = signalBus;
            _dotSettings = dotSettings;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<SelectedDotListChangedSignal>(SaveSelectedDots);
            _signalBus.Subscribe<SelectedDotsListClearedSignal>(TryMerge);
            _signalBus.Subscribe<FirstDotSelectedSignal>(SaveBaseValue);
        }

        private void SaveBaseValue(FirstDotSelectedSignal signal)
        {
            _baseValue = signal.Value;
        }

        private void TryMerge()
        {
            if (_selectedDotEntities == null || _selectedDotEntities.Length < 2)
            {
                FireMergeCompleteSignal();
                return;
            }

            int finalValue;
            
            if (_selectedDotEntities.Length < 4)
            {
                finalValue = _baseValue * 2;
            }
            
            else
            {
                finalValue = _baseValue * 4;
            }

            var lastSelectedDotEntity = _selectedDotEntities[^1];
            lastSelectedDotEntity.SetValue(finalValue);
            for (var i = 0; i < _selectedDotEntities.Length - 1; i++)
            {
                _selectedDotEntities[i].MergeTo(lastSelectedDotEntity);
            }

            DOVirtual.DelayedCall(_dotSettings.MergeMovementDuration, FireMergeCompleteSignal);
        }

        private void FireMergeCompleteSignal()
        {
            _signalBus.Fire<MergeCompleteSignal>();
            _selectedDotEntities = null;
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
            _signalBus.Unsubscribe<FirstDotSelectedSignal>(SaveBaseValue);
        }
    }
}