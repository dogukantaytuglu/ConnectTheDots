using System;
using System.Collections.Generic;
using Game.Features.Dot.Scripts.Dot;
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
            var lastSelectedDotValue = _selectedDotList[^1].CurrentValue;
            if (!TryGetDotEntityAtPosition(signal.InputPosition, out var dotEntity)) return;
            if (dotEntity.CurrentValue != lastSelectedDotValue) return;

            dotEntity.GetSelected();
            _selectedDotList.Add(dotEntity);
        }

        private void HandleFingerUp()
        {
            foreach (var dotEntity in _selectedDotList)
            {
                dotEntity.Deselect();
            }

            _selectedDotList.Clear();
        }

        private void HandleFingerDown(InputFingerDownSignal fingerDownSignal)
        {
            if (TryGetDotEntityAtPosition(fingerDownSignal.InputPosition, out var dotEntity))
            {
                dotEntity.GetSelected();
                _selectedDotList.Add(dotEntity);
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
    }
}