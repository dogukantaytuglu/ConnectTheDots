using Game.Features.Input.Scripts.Signals;
using UnityEngine;
using Zenject;

namespace Game.Features.Dot.Scripts.Systems
{
    public class DotInputHandler : IInitializable
    {
        private readonly SignalBus _signalBus;
        private readonly Camera _mainCamera;
        private readonly DotController _dotController;

        public DotInputHandler(SignalBus signalBus, Camera mainCamera, DotController dotController)
        {
            _signalBus = signalBus;
            _mainCamera = mainCamera;
            _dotController = dotController;
        }


        public void Initialize()
        {
            _signalBus.Subscribe<InputFingerDownSignal>(HandleFingerDown);
        }

        private void HandleFingerDown(InputFingerDownSignal fingerDownSignal)
        {
            var touchPosition = fingerDownSignal.InputPosition;
            var ray = _mainCamera.ScreenPointToRay(touchPosition);
            var hitInfo = Physics2D.Raycast(ray.origin, ray.direction);
            if (hitInfo.transform && hitInfo.transform.CompareTag(TagNames.Dot))
            {
                if (_dotController.TryGetDotEntity(hitInfo.transform, out var dotEntity))
                {
                    dotEntity.GetSelected();
                }
            }
        }
    }
}