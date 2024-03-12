using Game.Features.LineBetweenDots.Scripts.Line;
using Game.Features.LineBetweenDots.Scripts.Settings;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "LineBetweenDotsInstaller", menuName = "Installers/LineBetweenDotsInstaller")]
public class LineBetweenDotsInstaller : ScriptableObjectInstaller<LineBetweenDotsInstaller>
{
    [Inject] private LineBetweenDotsSettings _lineBetweenDotsSettings;
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<LineBetweenDotsHandler>()
            .FromSubContainerResolve()
            .ByNewPrefabInstaller<LineHandlerSubContainerInstaller>(_lineBetweenDotsSettings.LineRendererGameObject)
            .AsSingle().NonLazy();
    }
}