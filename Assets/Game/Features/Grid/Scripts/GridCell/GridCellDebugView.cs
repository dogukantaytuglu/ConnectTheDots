using TMPro;
using UnityEngine;

namespace Game.Features.Grid.Scripts.GridCell
{
    public class GridCellDebugView : MonoBehaviour
    {
        [SerializeField] private GameObject viewContainer;
        [SerializeField] private TextMeshPro debugText;
        
        public void SetActivate(bool value)
        {
            viewContainer.SetActive(value);
            var position = transform.position;
            debugText.text = $"{position.x}, {position.y}";
        }
    }
}
