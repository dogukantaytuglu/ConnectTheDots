using Game.Features.Grid.Scripts.GridCell;
using Game.Features.Grid.Scripts.Settings;
using Game.Features.Grid.Scripts.Systems;
using UnityEngine;
using Zenject;

namespace Game.Features.Grid.Scripts.Installer
{
    [CreateAssetMenu(fileName = "GridInstaller", menuName = "Installers/GridInstaller")]
    public class GridInstaller : ScriptableObjectInstaller<GridInstaller>
    {
        [Inject] private GridSettings _gridSettings;
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<GridGenerator>().AsSingle().NonLazy();
            Container.Bind<GridController>().AsSingle().NonLazy();
            
            Container.BindFactory<GridCellEntity, GridCellFactory>()
                .FromSubContainerResolve()
                .ByNewPrefabInstaller<GridCellInstaller>(_gridSettings.GridCellEntityPrefab)
                .UnderTransformGroup(_gridSettings.ParentName);
        }
    }
}