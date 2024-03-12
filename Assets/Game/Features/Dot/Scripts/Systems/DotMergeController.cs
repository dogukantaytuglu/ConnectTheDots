using System;
using Game.Features.Dot.Scripts.Dot;
using Game.Features.Dot.Scripts.Signals;
using Zenject;

namespace Game.Features.Dot.Scripts.Systems
{
    public class DotMergeController : IInitializable, IDisposable
    {
        private readonly SignalBus _signalBus;
        private DotEntity[] _selectedDotEntities;
        private int _baseValue;

        public DotMergeController(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<SelectedDotListChangedSignal>(SaveSelectedDots);
            _signalBus.Subscribe<SelectedDostListClearedSignal>(TryMerge);
            _signalBus.Subscribe<FirstDotSelectedSignal>(SaveBaseValue);
        }

        private void SaveBaseValue(FirstDotSelectedSignal signal)
        {
            _baseValue = signal.Value;
        }

        private void TryMerge()
        {
            if (_selectedDotEntities.Length < 2) return;

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
        }

        private void SaveSelectedDots(SelectedDotListChangedSignal signal)
        {
            _selectedDotEntities = signal.DotEntities;
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<SelectedDotListChangedSignal>(SaveSelectedDots);
            _signalBus.Unsubscribe<SelectedDostListClearedSignal>(TryMerge);
        }
    }
}