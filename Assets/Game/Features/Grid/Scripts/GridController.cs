using Game.Features.Grid.Scripts.GridCell;
using Game.GameSettings.Scripts;
using Zenject;

namespace Game.Features.Grid.Scripts
{
    public class GridController : IInitializable
    {
        private int _horizontalSize;
        private int _verticalSize;
        private GridCellFactory _gridCellFactory;
        
        public GridController(GridCellFactory gridCellFactory, GridSettings gridSettings)
        {
            _gridCellFactory = gridCellFactory;
            _horizontalSize = gridSettings.HorizontalGridSize;
            _verticalSize = gridSettings.VerticalGridSize;
        }
        
        public void Initialize()
        {
            var gridCell = _gridCellFactory.Create();
        }
    }
}
