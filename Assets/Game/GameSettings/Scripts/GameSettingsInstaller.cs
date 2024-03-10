using UnityEngine;
using Zenject;

namespace Game.GameSettings.Scripts
{
    [CreateAssetMenu(fileName = "GlobalGameSettings", menuName = "ScriptableObjects/CreateGlobalGameSettings", order = 1)]
    public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
    {
        [SerializeField] private GridSettings gridSettings;

        public override void InstallBindings()
        {
            Container.BindInstance(gridSettings);
        }
    }
}
