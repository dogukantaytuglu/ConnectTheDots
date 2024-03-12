using DG.Tweening;
using Game.Features.Dot.Scripts.Interfaces;
using Game.Features.Dot.Scripts.Systems;
using Game.Features.Grid.Scripts.GridCell;
using UnityEngine;
using Zenject;

namespace Game.Features.Dot.Scripts.Dot
{
    public class DotEntity : MonoBehaviour, ILineBetweenDotsRoutePoint
    {
        public int Value { get; private set; }
        public Vector2 DotCoordinate => _occupiedGridCellEntity.GridCoordinates;
        public Vector3 LineBetweenDotsRoutePoint => _occupiedGridCellEntity.transform.position;
        public Color Color => _dotVisualHandler.Color;

        private DotVisualHandler _dotVisualHandler;
        private DotController _dotController;
        private DotAnimationHandler _dotAnimationHandler;
        private GridCellEntity _occupiedGridCellEntity;

        [Inject]
        public void Construct(DotController dotController, DotVisualHandler dotVisualHandler, DotAnimationHandler dotAnimationHandler)
        {
            _dotController = dotController;
            _dotVisualHandler = dotVisualHandler;
            _dotAnimationHandler = dotAnimationHandler;
        }

        public void Initialize(int dotValue, GridCellEntity gridCellEntity)
        {
            _occupiedGridCellEntity = gridCellEntity;
            SetValue(dotValue);
            SetNameWithCoordinate(_occupiedGridCellEntity.GridCoordinates);
            _dotController.RegisterDotEntity(this);
        }

        public void SetValue(int value)
        {
            Value = value;
            _dotVisualHandler.HandleVisualByValue(value);
        }

        private void SetNameWithCoordinate(Vector2 gridCoordinates)
        {
            name = $"Dot {gridCoordinates}";
        }

        public void GetSelected()
        {
            _dotAnimationHandler.ScaleUp();
        }

        public void Deselect()
        {
            _dotAnimationHandler.ScaleDown();
        }

        public void MergeTo(DotEntity dotEntity)
        {
            var targetPosition = dotEntity.transform.position;
            targetPosition.z += 1;
            _dotAnimationHandler.MoveToPosition(targetPosition).OnComplete(DestroyThisDotEntity);
        }

        private void DestroyThisDotEntity()
        {
            _dotController.DeregisterDotEntity(this);
            Destroy(gameObject);
        }
    }
}
