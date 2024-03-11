using System.Collections.Generic;
using Game.Features.Grid.Scripts.GridCell;

namespace Game.Features.Grid.Scripts.Systems
{
    public class GridController
    {
        public int TotalGridCellCount => _allGridCells.Count;
        private readonly HashSet<GridCellEntity> _allGridCells = new();

        public void Register(GridCellEntity gridCellEntity)
        {
            if (_allGridCells.Add(gridCellEntity)) return;

            this.LogError(
                $"{gridCellEntity.name} is trying to register itself to the grid controller but it already exists!");
        }
    }
}