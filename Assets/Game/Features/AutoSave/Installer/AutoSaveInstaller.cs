using Game.Features.AutoSave.Systems;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "AutoSaveInstaller", menuName = "Installers/AutoSaveInstaller")]
public class AutoSaveInstaller : ScriptableObjectInstaller<AutoSaveInstaller>
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<AutoSaveSystem>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<LoadGameSystem>().AsSingle().NonLazy();
    }
}