using UnityEngine;

namespace Game.Features.Dot.Scripts.Signals
{
    public class FirstDotSelectedSignal
    {
        public Color Color;

        public FirstDotSelectedSignal(Color color)
        {
            Color = color;
        }
    }
}