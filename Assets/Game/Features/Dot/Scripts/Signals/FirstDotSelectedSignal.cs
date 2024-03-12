using UnityEngine;

namespace Game.Features.Dot.Scripts.Signals
{
    public class FirstDotSelectedSignal
    {
        public readonly Color Color;
        public readonly int Value;

        public FirstDotSelectedSignal(Color color, int value)
        {
            Color = color;
            Value = value;
        }
    }
}