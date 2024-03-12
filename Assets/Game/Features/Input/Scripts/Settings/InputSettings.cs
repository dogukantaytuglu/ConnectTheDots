using System;
using UnityEngine;

namespace Game.Features.Input.Scripts.Settings
{
    [Serializable]
    public class InputSettings
    {
        public float MinDragThreshold => minDragThreshold;

        [SerializeField] private float minDragThreshold;
    }
}