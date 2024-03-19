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
        [SerializeField] private GridSettings settings;
        public override void InstallBindings()
        {
            Container.BindInstance(settings);
            Container.BindInterfacesAndSelfTo<GridGenerator>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<GridController>().AsSingle().NonLazy();
            
            Container.BindFactory<GridCellEntity, GridCellFactory>()
                .FromSubContainerResolve()
                .ByNewPrefabInstaller<GridCellInstaller>(settings.GridCellEntityPrefab)
                .UnderTransformGroup(settings.ParentName);
        }
    }
}