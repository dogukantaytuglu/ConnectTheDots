using Game.Features.Dot.Scripts.Settings;
using TMPro;
using UnityEngine;
using Zenject;

namespace Game.Features.Dot.Scripts.Dot
{
    public class DotVisualHandler : MonoBehaviour
    {
        public Color Color => _color;
        [SerializeField] private GameObject innerRingGameObject;
        [SerializeField] private SpriteRenderer dotColorSprite;
        [SerializeField] private TextMeshPro valueText;

        private DotSettings _dotSettings;
        private Color _color;
        
        [Inject]
        public void Construct(DotSettings dotSettings)
        {
            _dotSettings = dotSettings;
        }
        
        public void HandleVisualByValue(int value)
        {
            _color = _dotSettings.DotColorPalette[CalculateIndex(value)];
            SetDotColor(_color);
            valueText.text = $"{value}";
            innerRingGameObject.SetActive(value > _dotSettings.DotVisualRingActivationThreshold);
        }

        private void SetDotColor(Color color)
        {
            dotColorSprite.color = color;
        }
        
        private int CalculateIndex(int value)
        {
            var valueOrder = (int)Mathf.Log(value, 2) - 1;
            return valueOrder % _dotSettings.DotColorPalette.Count;
        }
    }
}