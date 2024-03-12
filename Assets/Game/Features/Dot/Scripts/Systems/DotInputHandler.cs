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
        }

        private void HandleFingerUp()
        {
            foreach (var dotEntity in _selectedDotList)
            {
                dotEntity.Deselect();
            }
            
            _selectedDotList.Clear();
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<InputFingerDownSignal>(HandleFingerDown);
            _signalBus.Unsubscribe<InputFingerUpSignal>(HandleFingerUp);
        }

        private void HandleFingerDown(InputFingerDownSignal fingerDownSignal)
        {
            var touchPosition = fingerDownSignal.InputPosition;
            var ray = _mainCamera.ScreenPointToRay(touchPosition);
            var hitInfo = Physics2D.Raycast(ray.origin, ray.direction);
            if (IsInvalidHit(hitInfo)) return;
            if (!TrySelectDotEntity(hitInfo, out var dotEntity)) return;
            _selectedDotList.Add(dotEntity);
        }

        private static bool IsInvalidHit(RaycastHit2D hitInfo)
        {
            return !hitInfo.transform
                   || !hitInfo.transform.CompareTag(TagNames.Dot);
        }

        private bool TrySelectDotEntity(RaycastHit2D hitInfo, out DotEntity dotEntity)
        {
            if (_dotController.TryGetDotEntity(hitInfo.transform, out dotEntity))
            {
                dotEntity.GetSelected();
                return true;
            }

            return false;
        }
    }
}