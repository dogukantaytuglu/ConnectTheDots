using Game.Features.Grid.Scripts.Systems;
using Zenject;

namespace Game.Features.Dot.Scripts.Systems
{
    public class DotSpawner: IInitializable
    {
        private readonly GridController _gridController;

        public DotSpawner(GridController gridController)
        {
            _gridController = gridController;
        }
        
        private void SpawnRandomDots()
        {
            this.Log($"gridCellCount: {_gridController.TotalGridCellCount}");
        }

        public void Initialize()
        {
            SpawnRandomDots();
        }
    }
}