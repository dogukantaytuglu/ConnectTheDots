using System;
using System.Collections.Generic;
using Game.Features.Dot.Scripts.Dot;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Features.Dot.Scripts.Settings
{
    [Serializable]
    public class DotSettings
    {
        public List<Color> DotColorPalette => dotColorPalette;
        public DotEntity DotPrefab => dotPrefab;
        public List<int> StarterValues => starterValues;
        public int DotVisualRingActivationThreshold => dotVisualRingActivationThreshold;
        public string SpawnParentName => spawnParentName;
        public float ScaleDuration => scaleDuration;
        public float ScaleAmount => scaleAmount;
        public float MergeMovementDuration => mergeMovementDuration;

        public float DropDownMovementDuration => dropDownMovementDuration;

        [Header("Animation Parameters")] 
        [SerializeField] private float scaleDuration;
        [SerializeField] private float scaleAmount;
        [SerializeField] private float mergeMovementDuration;
        [SerializeField] private float dropDownMovementDuration;
        
        [Header("Others")]
        [SerializeField] private List<Color> dotColorPalette;
        [SerializeField] private List<int> starterValues;
        [SerializeField] private DotEntity dotPrefab;
        [SerializeField] private int dotVisualRingActivationThreshold = 1024;
        [SerializeField] private string spawnParentName;
    }
}
