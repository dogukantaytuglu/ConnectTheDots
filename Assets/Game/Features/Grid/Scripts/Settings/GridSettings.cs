using System;
using Game.Features.Grid.Scripts.GridCell;
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
        public GridCellEntity GridCellEntityPrefab => gridCellEntityPrefab;
        public float HorizontalSpaceBetweenCells => horizontalSpaceBetweenCells;
        public float VerticalSpaceBetweenCells => verticalSpaceBetweenCells;
        
        [Header("Grid Shape Properties")]
        [SerializeField] private int horizontalGridSize;
        [SerializeField] private int verticalGridSize;
        [SerializeField] private float horizontalSpaceBetweenCells;
        [SerializeField] private float verticalSpaceBetweenCells;
        [SerializeField] private float cellScale;
        [SerializeField] private float topMargin;
        [Header("Other Properties")]
        [SerializeField] private string parentName;
        [SerializeField] private GridCellEntity gridCellEntityPrefab;
        [SerializeField] private bool isDebugViewActive;
    }
}