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
        private float _debugTimeScale = 0.2f;

        public PlayerInputSystem(SignalBus signalBus, InputSettings inputSettings)
        {
            _signalBus = signalBus;
            _inputSettings = inputSettings;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<MergeCompleteSignal>(EnableInteraction);
        }

        public void Tick()
        {
            if (UnityEngine.Input.GetKey(KeyCode.LeftShift))
            {
                Time.timeScale = _debugTimeScale;
            }

            else if(Math.Abs(Time.timeScale - 1f) > 0.1f)
            {
                Time.timeScale = 1f;
            }
            
            if (!_canInteract) return;
            
            if (UnityEngine.Input.GetMouseButtonDown(0))
            {
                HandleMouseButtonDown();
            }

            if (UnityEngine.Input.GetMouseButton(0))
            {
                HandleMouseButton();
            }

            if (UnityEngine.Input.GetMouseButtonUp(0))
            {
                HandleMouseButtonUp();
            }
        }

        private void HandleMouseButtonUp()
        {
            DisableInteraction();
            _signalBus.Fire<InputFingerUpSignal>();
        }

        private void HandleMouseButton()
        {
            var mousePosition = UnityEngine.Input.mousePosition;
            var distance = Vector3.Distance(mousePosition, _lastDragPosition);
            if (distance > _inputSettings.MinDragThreshold)
            {
                _lastDragPosition = mousePosition;
                _signalBus.Fire(new InputFingerSignal(mousePosition));
            }
        }

        private void HandleMouseButtonDown()
        {
            var mousePosition = UnityEngine.Input.mousePosition;
            _signalBus.Fire(new InputFingerDownSignal(mousePosition));
            _lastDragPosition = mousePosition;
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<MergeCompleteSignal>(EnableInteraction);
        }
        
        private void EnableInteraction()
        {
            _canInteract = true;
        }

        private void DisableInteraction()
        {
            _canInteract = false;
        }
    }
}