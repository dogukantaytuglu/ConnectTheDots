using Game.Features.Grid.Scripts.GridCell;
using Zenject;

namespace Game.Features.Grid.Scripts.Installer
{
    public class GridCellInstaller : Installer<GridCellInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<GridCellDebugView>().AsSingle().NonLazy();
        }
    }
}