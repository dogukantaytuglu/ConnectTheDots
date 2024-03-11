using Zenject;

namespace Game.Features.Input.Scripts.Systems
{
    public class PlayerInputSystem : ITickable
    {
        public void Tick()
        {
            if (UnityEngine.Input.GetMouseButtonDown(0))
            {
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