using TMPro;
using UnityEngine;

namespace Game.Features.Grid.Scripts.GridCell
{
    public class GridCellDebugView : MonoBehaviour
    {
        [SerializeField] private GameObject viewContainer;
        [SerializeField] private TextMeshPro debugText;

        public void Initialize(bool isActive, Vector2 coordinates)
        {
            if (isActive)
            {
                Activate(coordinates);
            }

            else
            {
                Deactivate();
            }
        }
        
        private void Activate(Vector2 position)
        {
            debugText.text = $"{position.x},{position.y}";
            viewContainer.SetActive(true);
        }
        
        private void Deactivate()
        {
            viewContainer.SetActive(false);
        }
    }
}
