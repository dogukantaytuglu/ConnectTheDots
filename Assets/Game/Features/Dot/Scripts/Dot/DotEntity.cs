using DG.Tweening;
using Game.Features.Dot.Scripts.Systems;
using UnityEngine;
using Zenject;

namespace Game.Features.Dot.Scripts.Dot
{
    public class DotEntity : MonoBehaviour, IDotOnGrid
    {
        public int CurrentValue { get; private set; }

        private DotVisualHandler _dotVisualHandler;
        private DotController _dotController;
        
        [Inject]
        public void Construct(DotController dotController, DotVisualHandler dotVisualHandler)
        {
            _dotController = dotController;
            _dotVisualHandler = dotVisualHandler;
        }

        public void Initialize(int dotValue, Vector2 coordinates)
        {
            SetValue(dotValue);
            SetCoordinateOnGrid(coordinates);
            _dotController.RegisterDotEntity(this);
        }

        private void SetValue(int value)
        {
            CurrentValue = value;
            _dotVisualHandler.HandleVisualByValue(value);
        }

        private void SetCoordinateOnGrid(Vector2 gridCoordinates)
        {
            name = $"Dot {gridCoordinates}";
        }

        public void GetSelected()
        {
            transform.DOScale(1.2f ,0.2f);
        }
    }
}
