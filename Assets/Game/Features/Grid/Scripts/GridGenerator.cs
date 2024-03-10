using Game.Features.Grid.Scripts.GridCell;
using Game.GameSettings.Scripts;
using UnityEngine;
using Zenject;

namespace Game.Features.Grid.Scripts
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
            var xInitPosition = (_horizontalSize - 1) * -(_cellScale * 0.5f);
            var yInitPosition = (_verticalSize - 1) * -(_cellScale * 0.5f) - _topMargin;

            var initPosition = new Vector2(xInitPosition, yInitPosition);
            var targetPosition = initPosition;

            for (var i = 0; i < _horizontalSize; i++)
            {
                for (var j = 0; j < _verticalSize; j++)
                {
                    targetPosition.x = initPosition.x + i * _cellScale;
                    targetPosition.y = initPosition.y + j * _cellScale;

                    var gridCell = _gridCellFactory.Create();
                    gridCell.SetPosition(targetPosition);
                    gridCell.SetGridCoordinates(i, j);
                }
            }
        }
    }
}