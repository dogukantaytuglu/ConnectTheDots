using Game.Features.LineBetweenDots.Scripts.Line;
using Game.Features.LineBetweenDots.Scripts.Settings;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

[CreateAssetMenu(fileName = "LineBetweenDotsInstaller", menuName = "Installers/LineBetweenDotsInstaller")]
public class LineBetweenDotsInstaller : ScriptableObjectInstaller<LineBetweenDotsInstaller>
{
    [SerializeField] private LineBetweenDotsSettings lineBetweenDotsSettings;
    public override void InstallBindings()
    {
        Container.BindInstance(lineBetweenDotsSettings);

        Container.BindInterfacesAndSelfTo<LineBetweenDotsHandler>()
            .FromSubContainerResolve()
            .ByNewPrefabInstaller<LineHandlerSubContainerInstaller>(lineBetweenDotsSettings.LineRendererGameObject)
            .AsSingle().NonLazy();
    }
}