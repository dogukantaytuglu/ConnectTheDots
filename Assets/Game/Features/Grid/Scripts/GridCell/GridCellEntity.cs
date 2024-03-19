using Game.Features.Grid.Scripts.Interfaces;
using Game.Features.Grid.Scripts.Settings;
using Game.Features.Grid.Scripts.Systems;
using UnityEngine;
using Zenject;

namespace Game.Features.Grid.Scripts.GridCell
{
    public class GridCellEntity : MonoBehaviour
    {
        public Vector2 GridCoordinates { get; private set; }
        public IGridSpaceOccupier RegisteredOccupier { get; private set; }

        private GridCellDebugView _debugView;
        private GridController _gridController;
        private GridSettings _gridSettings;
        private bool _isDebugViewActive;

        [Inject]
        public void Construct(GridSettings gridSettings, GridCellDebugView gridCellDebugView, GridController gridController)
        {
            _gridSettings = gridSettings;
            _debugView = gridCellDebugView;
            _gridController = gridController;
        }

        public void Initialize(Vector2 position, Vector2 coordinates)
        {
            _isDebugViewActive= _gridSettings.IsDebugViewActive;
            transform.localScale = Vector3.one * _gridSettings.CellScale;
            SetPosition(position);
            SetGridCoordinates(coordinates);
            _gridController.RegisterGridCell(this);
        }

        private void SetPosition(Vector2 position)
        {
            var t = transform;
            var targetPosition = t.position;
            targetPosition.x = position.x;
            targetPosition.y = position.y;
            t.position = targetPosition;
        }

        private void SetGridCoordinates(Vector2 coordinates)
        {
            GridCoordinates = coordinates;

            gameObject.name = $"GridCell: {GridCoordinates}";
            _debugView.Initialize(_isDebugViewActive, GridCoordinates);
        }

        public void RegisterDot(IGridSpaceOccupier occupierToRegister)
        {
            _gridController.RemoveCellFromFreeList(this);
            RegisteredOccupier = occupierToRegister;
            occupierToRegister.CoordinateOnGrid = GridCoordinates;
        }

        public void DeregisterDot()
        {
            _gridController.AddCellToFreeList(this);
            RegisteredOccupier = null;
        }
    }
}