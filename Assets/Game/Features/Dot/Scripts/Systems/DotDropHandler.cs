using System;
using System.Collections.Generic;
using DG.Tweening;
using Game.Features.Dot.Scripts.Settings;
using Game.Features.Dot.Scripts.Signals;
using Game.Features.Grid.Scripts.GridCell;
using Game.Features.Grid.Scripts.Settings;
using Game.Features.Grid.Scripts.Systems;
using UnityEngine;
using Zenject;

namespace Game.Features.Dot.Scripts.Systems
{
    public class DotDropHandler : IInitializable, IDisposable
    {
        private readonly GridController _gridController;
        private readonly SignalBus _signalBus;
        private readonly List<GridCellEntity> _emptyCellBuffer = new();
        private readonly GridSettings _gridSettings;
        private readonly DotSettings _dotSettings;
        
        public DotDropHandler(GridController gridController, SignalBus signalBus, GridSettings gridSettings, DotSettings dotSettings)
        {
            _gridController = gridController;
            _signalBus = signalBus;
            _gridSettings = gridSettings;
            _dotSettings = dotSettings;
        }
        
        public void Initialize()
        {
            _signalBus.Subscribe<MergeCompleteSignal>(StartDotDropSequence);
        }
        
        private void StartDotDropSequence()
        {
            PopulateEmptyCellBuffer();

            foreach (var emptyCell in _emptyCellBuffer)
            {
                if (!emptyCell.IsGridCellFree) continue;
                DropDotsToEmptyCellsFromTop(emptyCell);
            }
        }
        
        private void PopulateEmptyCellBuffer()
        {
            _emptyCellBuffer.Clear();
            foreach (var gridCellEntity in _gridController.AllGridCells)
            {
                if (gridCellEntity.IsGridCellFree)
                {
                    _emptyCellBuffer.Add(gridCellEntity);
                }
            }
        }
        
        private void DropDotsToEmptyCellsFromTop(GridCellEntity emptyGridCell)
        {
            var spaceToDrop = 1;
            var coordinate = emptyGridCell.GridCoordinates;
            var targetCoordinate = new Vector2(coordinate.x, coordinate.y + 1);
            while (targetCoordinate.y < _gridSettings.VerticalGridSize)
            {
                var gridCellOnTop = _gridController.GridCellByCoordinateDictionary[targetCoordinate];
                if (_emptyCellBuffer.Contains(gridCellOnTop))
                {
                    spaceToDrop++;
                    targetCoordinate.y++;
                    continue;
                }

                if (gridCellOnTop.IsGridCellFree)
                {
                    targetCoordinate.y++;
                    continue;
                }

                var gridToDropDownCoordinate = targetCoordinate;
                gridToDropDownCoordinate.y -= spaceToDrop;
                var gridToDropDown = _gridController.GridCellByCoordinateDictionary[gridToDropDownCoordinate];
                gridCellOnTop.RegisteredDotEntity.DropDownTo(gridToDropDown);
                targetCoordinate.y++;
            }

            DOVirtual.DelayedCall(_dotSettings.DropDownMovementDuration, FireDotDropCompleteSignal);
        }

        private void FireDotDropCompleteSignal()
        {
            _signalBus.Fire<DotDropCompleteSignal>();
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<MergeCompleteSignal>(StartDotDropSequence);
        }
    }
}