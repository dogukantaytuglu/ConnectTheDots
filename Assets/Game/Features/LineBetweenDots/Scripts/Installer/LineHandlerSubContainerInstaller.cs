using Game.Features.LineBetweenDots.Scripts.Settings;
using UnityEngine;
using Zenject;

public class LineHandlerSubContainerInstaller : Installer<LineHandlerSubContainerInstaller>
{
    [Inject] private LineBetweenDotsSettings _lineBetweenDotsSettings;

    public override void InstallBindings()
    {
        Container.BindInstance(new Material(_lineBetweenDotsSettings.LineRendererMaterial));
    }
}