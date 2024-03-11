using Game.Features.Grid.Scripts.Settings;
using UnityEngine;
using Zenject;

namespace Game.Features.Grid.Scripts.GridCell
{
    public class GridCellEntity : MonoBehaviour
    {
        private GridCellDebugView _debugView;
        private bool _isDebugViewActive;
        private Vector2 _gridCoordinates = Vector2.zero;

        [Inject]
        public void Construct(GridSettings gridSettings, GridCellDebugView gridCellDebugView)
        {
            _isDebugViewActive= gridSettings.IsDebugViewActive;
            transform.localScale = Vector3.one * gridSettings.CellScale;
            _debugView = gridCellDebugView;
        }

        public void SetPosition(Vector2 position)
        {
            var t = transform;
            var targetPosition = t.position;
            targetPosition.x = position.x;
            targetPosition.y = position.y;
            t.position = targetPosition;
        }

        public void SetGridCoordinates(int x, int y)
        {
            _gridCoordinates.x = x;
            _gridCoordinates.y = y;

            gameObject.name = $"GridCell: {_gridCoordinates}";
            _debugView.Initialize(_isDebugViewActive, _gridCoordinates);
        }
    }
}