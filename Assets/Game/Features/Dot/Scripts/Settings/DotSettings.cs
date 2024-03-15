using System;
using System.Collections.Generic;
using Game.Features.Dot.Scripts.Dot;
using UnityEngine;

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

        public float MinBouncePower => minBouncePower;

        public float MaxBouncePower => maxBouncePower;
        public float BounceTotalDuration => bounceTotalDuration;
        public float SpawnAnimationDuration => spawnAnimationDuration;
        public float PopOnMergeDuration => popOnMergeDuration;
        public float PopOnMergeStrength => popOnMergeStrength;

        [Header("Scale Animation")] 
        [SerializeField] private float scaleDuration;
        [SerializeField] private float scaleAmount;
        
        [Header("Merge Animation")] 
        [SerializeField] private float mergeMovementDuration;
        [Header("Drop Down Animation")] 
        [SerializeField] private float dropDownMovementDuration;

        [Header("Bounce Animation")] 
        [SerializeField] private float minBouncePower;
        [SerializeField] private float maxBouncePower;
        [SerializeField] private float bounceTotalDuration;
        
        [Header("Spawn Animation")] 
        [SerializeField] private float spawnAnimationDuration;

        [Header("Pop On Merge Animation")]
        [SerializeField] private float popOnMergeDuration;
        [SerializeField] private float popOnMergeStrength;
        
        [Header("Others")]
        [SerializeField] private List<Color> dotColorPalette;
        [SerializeField] private List<int> starterValues;
        [SerializeField] private DotEntity dotPrefab;
        [SerializeField] private int dotVisualRingActivationThreshold = 1024;
        [SerializeField] private string spawnParentName;
    }
}
