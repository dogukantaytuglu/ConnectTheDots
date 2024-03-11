using UnityEngine;
using Zenject;

namespace Game.Features.Dot.Scripts.Dot
{
    public class DotEntity : MonoBehaviour, IDotOnGrid
    {
        public int CurrentValue { get; private set; }

        private DotVisualHandler _dotVisualHandler;
        
        [Inject]
        public void Construct(DotVisualHandler dotVisualHandler)
        {
            _dotVisualHandler = dotVisualHandler;
        }

        public void SetValue(int value)
        {
            CurrentValue = value;
            _dotVisualHandler.HandleVisualByValue(value);
        }
    }
}
