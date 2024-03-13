using System.Collections.Generic;
using Game.Features.Grid.Scripts.GridCell;
using UnityEngine;

namespace Game.Features.Grid.Scripts.Systems
{
    public class GridController
    {
        public HashSet<GridCellEntity> AllGridCells { get; } = new();
        public Dictionary<Vector2, GridCellEntity> GridCellByCoordinateDictionary { get; } = new();


        public void RegisterGridCell(GridCellEntity gridCellEntity)
        {
            if (!AllGridCells.Add(gridCellEntity))
            {
                this.LogError(
                    $"{gridCellEntity.name} is trying to register itself to the grid controller but it already exists!");
                return;
            }

            GridCellByCoordinateDictionary.Add(gridCellEntity.GridCoordinates, gridCellEntity);
        }

        public bool TryGetFreeCellEntity(out GridCellEntity freeCellEntity)
        {
            freeCellEntity = null;
            foreach (var gridCellEntity in AllGridCells)
            {
                if (gridCellEntity.IsGridCellFree)
                {
                    freeCellEntity = gridCellEntity;
                    return true;
                }
            }

            return false;
        }
    }
}