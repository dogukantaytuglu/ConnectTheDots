using Game.Features.Dot.Scripts.Dot;
using Game.Features.Grid.Scripts.Systems;
using Zenject;

namespace Game.Features.Dot.Scripts.Systems
{
    public class DotSpawner: IInitializable
    {
        private readonly GridController _gridController;
        private readonly DotFactory _dotFactory;

        public DotSpawner(GridController gridController, DotFactory dotFactory)
        {
            _gridController = gridController;
            _dotFactory = dotFactory;
        }
        
        public void Initialize()
        {
            PopulateGridWithDots();
        }
        
        private void PopulateGridWithDots()
        {
            while (_gridController.TryGetFreeCellEntity(out var freeCellEntity))
            {
                var dotEntity = _dotFactory.Create();
                dotEntity.transform.position = freeCellEntity.transform.position;
                _gridController.RegisterDotToGridCell(freeCellEntity, dotEntity);
            }
        }
    }
}