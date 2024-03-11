using System;
using UnityEngine;

namespace Game.Features.Dot.Scripts.Settings
{
    [Serializable]
    public class DotStateProperty
    {
        public int StateValue => stateValue;

        public Color StateColor => stateColor;

        public bool HasInnerCircle => hasInnerCircle;

        [SerializeField] private int stateValue;
        [SerializeField] private Color stateColor;
        [SerializeField] private bool hasInnerCircle;
    }
}