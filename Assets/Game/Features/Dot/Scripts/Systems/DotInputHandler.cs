using System;
using System.Collections.Generic;
using Game.Features.Dot.Scripts.Dot;
using Game.Features.Dot.Scripts.Signals;
using Game.Features.Input.Scripts.Signals;
using UnityEngine;
using Zenject;

namespace Game.Features.Dot.Scripts.Systems
{
    public class DotInputHandler : IInitializable, IDisposable
    {
        private readonly SignalBus _signalBus;
        private readonly Camera _mainCamera;
        private readonly DotController _dotController;
        private readonly List<DotEntity> _selectedDotList = new();

        public DotInputHandler(SignalBus signalBus, Camera mainCamera, DotController dotController)
        {
            _signalBus = signalBus;
            _mainCamera = mainCamera;
            _dotController = dotController;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<InputFingerDownSignal>(HandleFingerDown);
            _signalBus.Subscribe<InputFingerUpSignal>(HandleFingerUp);
            _signalBus.Subscribe<InputFingerSignal>(HandleFingerMovement);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<InputFingerDownSignal>(HandleFingerDown);
            _signalBus.Unsubscribe<InputFingerUpSignal>(HandleFingerUp);
            _signalBus.Unsubscribe<InputFingerSignal>(HandleFingerMovement);
        }

        private void HandleFingerMovement(InputFingerSignal signal)
        {
            if (_selectedDotList.Count < 1) return;
            var lastSelectedDot = _selectedDotList[^1];
            var lastSelectedDotValue = lastSelectedDot.Value;
            if (!TryGetDotEntityAtPosition(signal.InputPosition, out var dotEntity)) return;
            if (TryDeselect(dotEntity)) return;
            if (dotEntity.Value != lastSelectedDotValue) return;
            if (!_dotController.IsNeighbourDot(lastSelectedDot, dotEntity)) return;

            SelectDotEntity(dotEntity);
        }
        
        private void HandleFingerUp()
        {
            foreach (var dotEntity in _selectedDotList)
            {
                dotEntity.Deselect();
            }

            _selectedDotList.Clear();
            _signalBus.Fire<SelectedDostListClearedSignal>();
        }

        private void HandleFingerDown(InputFingerDownSignal fingerDownSignal)
        {
            if (TryGetDotEntityAtPosition(fingerDownSignal.InputPosition, out var dotEntity))
            {
                SelectDotEntity(dotEntity);
                _signalBus.Fire(new FirstDotSelectedSignal(dotEntity.Color, dotEntity.Value));
            }
        }

        private bool TryGetDotEntityAtPosition(Vector3 screenPosition, out DotEntity dotEntity)
        {
            dotEntity = null;
            var ray = _mainCamera.ScreenPointToRay(screenPosition);
            var hitInfo = Physics2D.Raycast(ray.origin, ray.direction);
            return !IsInvalidHit(hitInfo) && TryGetDotEntity(hitInfo, out dotEntity);
        }

        private static bool IsInvalidHit(RaycastHit2D hitInfo)
        {
            return !hitInfo.transform
                   || !hitInfo.transform.CompareTag(TagNames.Dot);
        }

        private bool TryGetDotEntity(RaycastHit2D hitInfo, out DotEntity dotEntity)
        {
            dotEntity = null;
            return _dotController.TryGetDotEntity(hitInfo.transform, out dotEntity);
        }

        private void SelectDotEntity(DotEntity dotEntity)
        {
            if (_selectedDotList.Contains(dotEntity)) return;
            dotEntity.GetSelected();
            _selectedDotList.Add(dotEntity);
            FireSelectedDotListChangedSignal();
        }
        
        private bool TryDeselect(DotEntity hoveredDotEntity)
        {
            if (_selectedDotList.Count < 2) return false;
            if (hoveredDotEntity == _selectedDotList[^2])
            {
                var lastAddedDotEntity = _selectedDotList[^1];
                lastAddedDotEntity.Deselect();
                _selectedDotList.Remove(lastAddedDotEntity);
                FireSelectedDotListChangedSignal();
                return true;
            }

            return false;
        }
        
        private void FireSelectedDotListChangedSignal()
        {
            _signalBus.Fire(new SelectedDotListChangedSignal(_selectedDotList.ToArray()));
        }
    }
}