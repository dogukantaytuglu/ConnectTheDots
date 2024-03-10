using UnityEngine;
using Zenject;

namespace Game.Installer
{
    [CreateAssetMenu(fileName = "GameInstaller", menuName = "Installers/GameInstaller")]
    public class GameInstaller : ScriptableObjectInstaller<GameInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<GameManager>().AsSingle().NonLazy();
            SignalBusInstaller.Install(Container);
        }
    }
}