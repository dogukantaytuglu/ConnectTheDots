using System.Collections.Generic;
using Game.Features.Dot.Scripts.Settings;
using UnityEngine;
using Zenject;

namespace Game.Features.Dot.Scripts.Dot
{
    public class DotEntity : MonoBehaviour
    {
        public int CurrentValue { get; private set; }
        
        [SerializeField] private GameObject innerRingGameObject;
        [SerializeField] private SpriteRenderer dotColorSprite;

        private List<Color> _colorPalette;
        
        [Inject]
        public void Construct(DotSettings dotSettings)
        {
            _colorPalette = dotSettings.DotColorPalette;
        }

        public void SetValue(int value)
        {
            CurrentValue = value;
            var color = _colorPalette[CalculateIndex(value)];
            SetDotColor(color);
        }

        private void SetDotColor(Color color)
        {
            dotColorSprite.color = color;
        }
        
        private int CalculateIndex(int value)
        {
            var valueOrder = (int)Mathf.Log(value, 2) - 1;
            return valueOrder % _colorPalette.Count;
        }
    }
}
