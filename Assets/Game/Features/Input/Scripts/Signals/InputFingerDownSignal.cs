using UnityEngine;

namespace Game.Features.Input.Scripts.Signals
{
    public class InputFingerDownSignal
    {
        public readonly Vector3 InputPosition;

        public InputFingerDownSignal(Vector3 inputPosition)
        {
            InputPosition = inputPosition;
        }
    }
}