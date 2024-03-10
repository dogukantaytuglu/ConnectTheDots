using System;
using UnityEngine;

namespace Game.GameSettings.Scripts
{
    [Serializable]
    public class GridSettings
    {
        public int HorizontalGridSize => horizontalGridSize;
        public int VerticalGridSize => verticalGridSize;
        public bool IsDebugViewActive => isDebugViewActive;
        
        [SerializeField] private int horizontalGridSize;
        [SerializeField] private int verticalGridSize;
        [SerializeField] private bool isDebugViewActive;
    }
}