using Game.Features.Input.Scripts.Settings;
using Game.Features.Input.Scripts.Signals;
using Game.Features.Input.Scripts.Systems;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "InputSystemInstaller", menuName = "Installers/InputSystemInstaller")]
public class InputSystemInstaller : ScriptableObjectInstaller<InputSystemInstaller>
{
    [SerializeField] private InputSettings inputSettings;
    public override void InstallBindings()
    {
        Container.BindInstance(inputSettings);
        Container.BindInterfacesAndSelfTo<PlayerInputSystem>().AsSingle().NonLazy();
        Container.DeclareSignal<InputFingerDownSignal>();
        Container.DeclareSignal<InputFingerUpSignal>();
        Container.DeclareSignal<InputFingerSignal>();
    }
}