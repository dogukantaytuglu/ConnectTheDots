using Game.GameSettings.Scripts;
using UnityEngine;
using Zenject;

namespace Game.Features.Grid.Scripts.GridCell
{
    public class GridCellEntity : MonoBehaviour
    {
        [SerializeField] private GridCellDebugView _debugView;

        [Inject]
        public void Construct(GridSettings gridSettings)
        {
            _debugView.SetActivate(gridSettings.IsDebugViewActive);
        }
    }
}