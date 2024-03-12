using Game.Features.Dot.Scripts.Settings;
using Game.Features.Grid.Scripts.Settings;
using Game.Features.Input.Scripts.Settings;
using UnityEngine;
using Zenject;

namespace Game.GameSettings.Scripts
{
    [CreateAssetMenu(fileName = "GlobalGameSettings", menuName = "ScriptableObjects/CreateGlobalGameSettings", order = 1)]
    public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
    {
        [SerializeField] private GridSettings gridSettings;
        [SerializeField] private DotSettings dotSettings;
        [SerializeField] private InputSettings inputSettings;

        public override void InstallBindings()
        {
            Container.BindInstance(gridSettings);
            Container.BindInstance(dotSettings);
            Container.BindInstance(inputSettings);
        }
    }
}
