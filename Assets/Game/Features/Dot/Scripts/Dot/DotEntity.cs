using Game.Features.Dot.Scripts.Systems;
using Game.Features.Grid.Scripts.GridCell;
using Game.Features.Grid.Scripts.Interfaces;
using UnityEngine;
using Zenject;

namespace Game.Features.Dot.Scripts.Dot
{
    public class DotEntity : MonoBehaviour, IGridSpaceOccupier, IPoolable<IMemoryPool>
    {
        public int Value { get; private set; }
        public Vector2 CoordinateOnGrid { get; set; }
        public Color Color => _dotVisualHandler.Color;

        private DotVisualHandler _dotVisualHandler;
        private DotController _dotController;
        private DotAnimationHandler _dotAnimationHandler;
        private GridCellEntity _occupiedGridCellEntity;
        private IMemoryPool _pool;

        [Inject]
        public void Construct(DotController dotController, DotVisualHandler dotVisualHandler, DotAnimationHandler dotAnimationHandler)
        {
            _dotController = dotController;
            _dotVisualHandler = dotVisualHandler;
            _dotAnimationHandler = dotAnimationHandler;
        }

        public void Initialize(int dotValue, GridCellEntity gridCellEntity)
        {
            _dotAnimationHandler.PlaySpawnAnimation();
            _occupiedGridCellEntity = gridCellEntity;
            SetValue(dotValue);
            SetNameWithCoordinate(_occupiedGridCellEntity.GridCoordinates);
            _dotController.RegisterDotEntity(this);
            _occupiedGridCellEntity.RegisterDot(this);
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
            _dotAnimationHandler.ScaleDown();
            _dotAnimationHandler.MoveToMergePosition(targetPosition, Despawn);
        }

        private void Despawn()
        {
            _pool.Despawn(this);
            _occupiedGridCellEntity.DeregisterDot();
        }

        public void OnDespawned()
        {
            _dotController.DeregisterDotEntity(this);
            _pool = null;
        }

        public void OnSpawned(IMemoryPool pool)
        {
            _pool = pool;
        }

        public void DropDownTo(GridCellEntity gridCellEntity)
        {
            _occupiedGridCellEntity.DeregisterDot();
            _occupiedGridCellEntity = gridCellEntity;
            _occupiedGridCellEntity.RegisterDot(this);
            _dotAnimationHandler.MoveToDropPosition(gridCellEntity.transform.position);
        }

        public void PlayPopAnimation()
        {
            _dotAnimationHandler.PlayPopAnimation();
        }

    }
}
