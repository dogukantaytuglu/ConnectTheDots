using Game.Features.Dot.Scripts.Dot;
using Game.Features.Dot.Scripts.Settings;
using Game.Features.Dot.Scripts.Signals;
using Game.Features.Dot.Scripts.Systems;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "DotInstaller", menuName = "Installers/DotInstaller")]
public class DotInstaller : ScriptableObjectInstaller<DotInstaller>
{
    [Inject] private DotSettings _dotSettings;
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<DotInputHandler>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<DotController>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<DotMergeController>().AsSingle().NonLazy();
        Container.Bind<DotSpawner>().AsSingle().NonLazy();
        
        Container.DeclareSignal<SelectedDotListChangedSignal>();
        Container.DeclareSignal<FirstDotSelectedSignal>();
        Container.DeclareSignal<SelectedDostListClearedSignal>();
        
        Container.BindFactory<DotEntity, DotFactory>()
            .FromSubContainerResolve()
            .ByNewPrefabInstaller<DotEntityInstaller>(_dotSettings.DotPrefab)
            .UnderTransformGroup(_dotSettings.SpawnParentName);
    }
}