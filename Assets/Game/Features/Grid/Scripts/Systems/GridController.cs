using System.Collections.Generic;
using Game.Features.Dot.Scripts.Dot;
using Game.Features.Grid.Scripts.GridCell;

namespace Game.Features.Grid.Scripts.Systems
{
    public class GridController
    {
        private readonly HashSet<GridCellEntity> _allGridCells = new();

        public void RegisterGridCell(GridCellEntity gridCellEntity)
        {
            if (_allGridCells.Add(gridCellEntity)) return;

            this.LogError(
                $"{gridCellEntity.name} is trying to register itself to the grid controller but it already exists!");
        }

        public bool TryGetFreeCellEntity(out GridCellEntity freeCellEntity)
        {
            freeCellEntity = null;
            foreach (var gridCellEntity in _allGridCells)
            {
                if (gridCellEntity.IsGridCellFree)
                {
                    freeCellEntity = gridCellEntity;
                    return true;
                }
            }

            return false;
        }

        public void RegisterDotToGridCell(GridCellEntity gridCellEntity, IDotOnGrid dotToRegister)
        {
            gridCellEntity.RegisterDot(dotToRegister);
        }
    }
}