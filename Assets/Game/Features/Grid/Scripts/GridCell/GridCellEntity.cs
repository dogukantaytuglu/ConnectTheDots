using Game.Features.Grid.Scripts.Data;
using Game.Features.Grid.Scripts.Settings;
using Game.Features.Grid.Scripts.Systems;
using UnityEngine;
using Zenject;

namespace Game.Features.Grid.Scripts.GridCell
{
    public class GridCellEntity : MonoBehaviour
    {
        public GridCellState GridCellState { get; private set; }
        private GridCellDebugView _debugView;
        private bool _isDebugViewActive;
        private Vector2 _gridCoordinates = Vector2.zero;
        private GridController _gridController;
        private GridSettings _gridSettings;

        [Inject]
        public void Construct(GridSettings gridSettings, GridCellDebugView gridCellDebugView, GridController gridController)
        {
            _gridSettings = gridSettings;
            _debugView = gridCellDebugView;
            _gridController = gridController;
        }
        
        public void Awake()
        {
            _isDebugViewActive= _gridSettings.IsDebugViewActive;
            transform.localScale = Vector3.one * _gridSettings.CellScale;
            _gridController.Register(this);
        }

        public void Initialize(Vector2 position, Vector2 coordinates)
        {
            SetPosition(position);
            SetGridCoordinates(coordinates);
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
            _gridCoordinates.x = coordinates.x;
            _gridCoordinates.y = coordinates.y;

            gameObject.name = $"GridCell: {_gridCoordinates}";
            _debugView.Initialize(_isDebugViewActive, _gridCoordinates);
        }
    }
}