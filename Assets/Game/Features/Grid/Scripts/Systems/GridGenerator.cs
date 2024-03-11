using Game.Features.Grid.Scripts.GridCell;
using Game.Features.Grid.Scripts.Settings;
using UnityEngine;
using Zenject;

namespace Game.Features.Grid.Scripts.Systems
{
    public class GridGenerator : IInitializable
    {
        private int _horizontalSize;
        private int _verticalSize;
        private float _cellScale;
        private float _topMargin;
        private GridCellFactory _gridCellFactory;

        public GridGenerator(GridCellFactory gridCellFactory, GridSettings gridSettings)
        {
            _gridCellFactory = gridCellFactory;
            _horizontalSize = gridSettings.HorizontalGridSize;
            _verticalSize = gridSettings.VerticalGridSize;
            _cellScale = gridSettings.CellScale;
            _topMargin = gridSettings.TopMargin;
        }

        public void Initialize()
        {
            GenerateGridCells();
        }

        private void GenerateGridCells()
        {
            var initPosition = CalculateInitPosition();
            var coordinates = Vector2.zero;

            for (var i = 0; i < _horizontalSize; i++)
            {
                coordinates.x = i;
                for (var j = 0; j < _verticalSize; j++)
                {
                    coordinates.y = j;
                    var targetPosition = CalculateTargetPosition(initPosition, coordinates);

                    var gridCell = _gridCellFactory.Create();
                    gridCell.Initialize(targetPosition, coordinates);
                }
            }
        }

        private Vector2 CalculateTargetPosition(Vector2 initPosition, Vector2 coordinates)
        {
            Vector2 targetPosition;
            targetPosition.x = initPosition.x + coordinates.x * _cellScale;
            targetPosition.y = initPosition.y + coordinates.y * _cellScale;
            return targetPosition;
        }

        private Vector2 CalculateInitPosition()
        {
            var xInitPosition = (_horizontalSize - 1) * -(_cellScale * 0.5f);
            var yInitPosition = (_verticalSize - 1) * -(_cellScale * 0.5f) - _topMargin;

            var initPosition = new Vector2(xInitPosition, yInitPosition);
            return initPosition;
        }
    }
}