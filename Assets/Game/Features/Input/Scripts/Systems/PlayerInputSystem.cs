using Game.Features.Input.Scripts.Signals;
using Zenject;

namespace Game.Features.Input.Scripts.Systems
{
    public class PlayerInputSystem : ITickable
    {
        private readonly SignalBus _signalBus;

        public PlayerInputSystem(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }
        public void Tick()
        {
            if (UnityEngine.Input.GetMouseButtonDown(0))
            {
                var touchPosition = UnityEngine.Input.mousePosition;
                _signalBus.Fire(new InputFingerDownSignal(touchPosition));
            }
            if (UnityEngine.Input.GetMouseButton(0))
            {
            }
            if (UnityEngine.Input.GetMouseButtonUp(0))
            {
            }
        }
    }
}