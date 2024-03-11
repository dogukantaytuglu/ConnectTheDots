using System;
using UnityEngine;

namespace Game.Features.Grid.Scripts.Settings
{
    [Serializable]
    public class GridSettings
    {
        public int HorizontalGridSize => horizontalGridSize;
        public int VerticalGridSize => verticalGridSize;
        public bool IsDebugViewActive => isDebugViewActive;
        public float CellScale => cellScale;
        public float TopMargin => topMargin;
        public string ParentName => parentName;

        [SerializeField] private int horizontalGridSize;
        [SerializeField] private int verticalGridSize;
        [SerializeField] private bool isDebugViewActive;
        [SerializeField] private float cellScale;
        [SerializeField] private float topMargin;
        [SerializeField] private string parentName;
    }
}