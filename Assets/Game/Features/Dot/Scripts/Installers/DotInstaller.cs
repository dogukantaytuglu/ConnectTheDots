using Game.Features.Dot.Scripts.Dot;
using Game.Features.Dot.Scripts.Settings;
using Game.Features.Dot.Scripts.Signals;
using Game.Features.Dot.Scripts.Systems;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

[CreateAssetMenu(fileName = "DotInstaller", menuName = "Installers/DotInstaller")]
public class DotInstaller : ScriptableObjectInstaller<DotInstaller>
{
    [SerializeField] private DotSettings dotSettings;

    public override void InstallBindings()
    {
        Container.BindInstance(dotSettings);
        Container.BindInterfacesAndSelfTo<DotSelectionHandler>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<DotController>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<DotMergeController>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<DotSpawner>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<DotDropHandler>().AsSingle().NonLazy();
        
        Container.Bind<DotValueTextConverter>().AsSingle().NonLazy();

        Container.DeclareSignal<SelectedDotListChangedSignal>();
        Container.DeclareSignal<FirstDotSelectedSignal>();
        Container.DeclareSignal<SelectedDotsListClearedSignal>();
        Container.DeclareSignal<MergeCompleteSignal>();
        Container.DeclareSignal<DotDropCompleteSignal>();

        Container.BindFactory<DotEntity, DotFactory>()
            .FromPoolableMemoryPool<DotEntity, DotPool>(poolBinder => poolBinder
                .WithInitialSize(35)
                .FromSubContainerResolve()
                .ByNewPrefabInstaller<DotEntityInstaller>(dotSettings.DotPrefab)
                .UnderTransformGroup(dotSettings.SpawnParentName));
    }
}