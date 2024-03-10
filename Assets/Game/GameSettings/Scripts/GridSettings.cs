using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.GameSettings.Scripts
{
    [Serializable]
    public class GridSettings
    {
        public int HorizontalGridSize => horizontalGridSize;
        public int VerticalGridSize => verticalGridSize;
        public bool IsDebugViewActive => isDebugViewActive;
        public float CellScale => cellScale;
        public float TopMargin => topMargin;

        [SerializeField] private int horizontalGridSize;
        [SerializeField] private int verticalGridSize;
        [SerializeField] private bool isDebugViewActive;
        [SerializeField] private float cellScale;
        [SerializeField] private float topMargin;
    }
}