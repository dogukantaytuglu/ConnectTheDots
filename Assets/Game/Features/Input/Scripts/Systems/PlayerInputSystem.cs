using System;
using Game.Features.Dot.Scripts.Signals;
using Game.Features.Input.Scripts.Settings;
using Game.Features.Input.Scripts.Signals;
using UnityEngine;
using Zenject;

namespace Game.Features.Input.Scripts.Systems
{
    public class PlayerInputSystem : IInitializable, IDisposable, ITickable
    {
        private readonly SignalBus _signalBus;
        private readonly InputSettings _inputSettings;
        private Vector3 _lastDragPosition;
        private bool _canInteract = true;

        public PlayerInputSystem(SignalBus signalBus, InputSettings inputSettings)
        {
            _signalBus = signalBus;
            _inputSettings = inputSettings;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<MergeCompleteSignal>(EnableInteraction);
        }

        private void EnableInteraction()
        {
            _canInteract = true;
        }

        public void Tick()
        {
            if (!_canInteract) return;
            
            if (UnityEngine.Input.GetMouseButtonDown(0))
            {
                var mousePosition = UnityEngine.Input.mousePosition;
                _signalBus.Fire(new InputFingerDownSignal(mousePosition));
                _lastDragPosition = mousePosition;
            }

            if (UnityEngine.Input.GetMouseButton(0))
            {
                var mousePosition = UnityEngine.Input.mousePosition;
                var distance = Vector3.Distance(mousePosition, _lastDragPosition);
                if (distance > _inputSettings.MinDragThreshold)
                {
                    _lastDragPosition = mousePosition;
                    _signalBus.Fire(new InputFingerSignal(mousePosition));
                }
            }

            if (UnityEngine.Input.GetMouseButtonUp(0))
            {
                _canInteract = false;
                _signalBus.Fire<InputFingerUpSignal>();
            }
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<MergeCompleteSignal>(EnableInteraction);
        }
    }
}