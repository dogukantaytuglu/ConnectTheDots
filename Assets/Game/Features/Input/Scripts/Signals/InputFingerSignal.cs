using UnityEngine;

namespace Game.Features.Input.Scripts.Signals
{
    public class InputFingerSignal
    {
        public readonly Vector3 InputPosition;

        public InputFingerSignal(Vector3 inputPosition)
        {
            InputPosition = inputPosition;
        }
    }
}