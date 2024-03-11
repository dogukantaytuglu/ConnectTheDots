using Game.Features.Dot.Scripts.Systems;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "DotInstaller", menuName = "Installers/DotInstaller")]
public class DotInstaller : ScriptableObjectInstaller<DotInstaller>
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<DotSpawner>().AsSingle().NonLazy();
    }
}