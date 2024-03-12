using Game.Features.Input.Scripts.Signals;
using Game.Features.Input.Scripts.Systems;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "InputSystemInstaller", menuName = "Installers/InputSystemInstaller")]
public class InputSystemInstaller : ScriptableObjectInstaller<InputSystemInstaller>
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<PlayerInputSystem>().AsSingle().NonLazy();
        Container.DeclareSignal<InputFingerDownSignal>();
    }
}