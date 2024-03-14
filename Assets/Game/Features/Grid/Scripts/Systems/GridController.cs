using System.Collections.Generic;
using Game.Features.Grid.Scripts.GridCell;
using UnityEngine;

namespace Game.Features.Grid.Scripts.Systems
{
    public class GridController
    {
        public HashSet<GridCellEntity> AllGridCells { get; } = new();
        public Dictionary<Vector2, GridCellEntity> GridCellByCoordinateDictionary { get; } = new();
        private List<GridCellEntity> FreeGridCells { get; } = new();


        public void RegisterGridCell(GridCellEntity gridCellEntity)
        {
            if (!AllGridCells.Add(gridCellEntity))
            {
                this.LogError(
                    $"{gridCellEntity.name} is trying to register itself to the grid controller but it already exists!");
                return;
            }

            FreeGridCells.Add(gridCellEntity);
            GridCellByCoordinateDictionary.Add(gridCellEntity.GridCoordinates, gridCellEntity);
        }

        public void AddCellToFreeList(GridCellEntity gridCellEntity)
        {
            if (IsGridCellFree(gridCellEntity))
            {
                this.LogError($"{gridCellEntity.GridCoordinates} tried to add itself to free list more than once!");
                return;
            }

            FreeGridCells.Add(gridCellEntity);
        }

        public void RemoveCellFromFreeList(GridCellEntity gridCellEntity)
        {
            if (!FreeGridCells.Remove(gridCellEntity))
            {
                this.LogError($"{gridCellEntity.GridCoordinates} tries to remove itself from free list but it does not exist in the list!");
            }
        }

        public bool TryGetFreeCellEntity(out GridCellEntity freeCellEntity)
        {
            freeCellEntity = null;
            if (FreeGridCells.Count < 1) return false;

            freeCellEntity = FreeGridCells[0];
            return true;
        }

        public bool IsGridCellFree(GridCellEntity gridCellEntity)
        {
            return FreeGridCells.Contains(gridCellEntity);
        }

        public bool TryGetGridCellByCoordinate(Vector2 dataCoordinate, out GridCellEntity gridCellEntity)
        {
            gridCellEntity = null;
            foreach (var gridCell in AllGridCells)
            {
                if (gridCell.GridCoordinates != dataCoordinate) continue;
                gridCellEntity = gridCell;
                return true;
            }

            this.LogError($"Tried to get cell by coordinate {dataCoordinate} but cell not found");
            return false;
        }
    }
}