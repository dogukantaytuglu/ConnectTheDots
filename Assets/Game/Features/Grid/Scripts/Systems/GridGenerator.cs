using Game.Features.Grid.Scripts.GridCell;
using Game.Features.Grid.Scripts.Settings;
using UnityEngine;
using Zenject;

namespace Game.Features.Grid.Scripts.Systems
{
    public class GridGenerator : IInitializable
    {
        private readonly GridCellFactory _gridCellFactory;
        private readonly GridSettings _gridSettings;
        private float HorizontalCellSize => _gridSettings.CellScale + _gridSettings.HorizontalSpaceBetweenCells;
        private float VerticalCellSize => _gridSettings.CellScale + _gridSettings.VerticalSpaceBetweenCells;

        public GridGenerator(GridCellFactory gridCellFactory, GridSettings gridSettings)
        {
            _gridCellFactory = gridCellFactory;
            _gridSettings = gridSettings;
        }

        public void Initialize()
        {
            GenerateGridCells();
        }

        private void GenerateGridCells()
        {
            var horizontalSize = _gridSettings.HorizontalGridSize;
            var verticalSize = _gridSettings.VerticalGridSize;
            var initPosition = CalculateInitPosition();
            var coordinates = Vector2.zero;

            for (var i = 0; i < horizontalSize; i++)
            {
                coordinates.x = i;
                for (var j = 0; j < verticalSize; j++)
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
            targetPosition.x = initPosition.x + coordinates.x * HorizontalCellSize;
            targetPosition.y = initPosition.y + coordinates.y * VerticalCellSize;
            return targetPosition;
        }

        private Vector2 CalculateInitPosition()
        {
            var horizontalSize = _gridSettings.HorizontalGridSize;
            var verticalSize = _gridSettings.VerticalGridSize;
            var topMargin = _gridSettings.TopMargin;
            
            var xInitPosition = (horizontalSize - 1) * -(HorizontalCellSize * 0.5f);
            var yInitPosition = (verticalSize - 1) * -(VerticalCellSize * 0.5f) - topMargin;

            var initPosition = new Vector2(xInitPosition, yInitPosition);
            return initPosition;
        }
    }
}