using Game.Features.Grid.Scripts.GridCell;
using UnityEngine;
using Zenject;

namespace Game.Features.Grid.Scripts.Installer
{
    [CreateAssetMenu(fileName = "GridInstaller", menuName = "Installers/GridInstaller")]
    public class GridInstaller : ScriptableObjectInstaller<GridInstaller>
    {
        [SerializeField] private GridCellEntity gridCellEntityPrefab;
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<GridController>().AsSingle().NonLazy();
            Container.BindFactory<GridCellEntity, GridCellFactory>().FromComponentInNewPrefab(gridCellEntityPrefab).UnderTransformGroup("CellEntities");
        }
    }
}