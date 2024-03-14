using System;
using Game.Features.AutoSave.Data;
using Game.Features.AutoSave.Systems;
using Game.Features.Dot.Scripts.Dot;
using Game.Features.Dot.Scripts.Settings;
using Game.Features.Dot.Scripts.Signals;
using Game.Features.Grid.Scripts.GridCell;
using Game.Features.Grid.Scripts.Systems;
using Zenject;
using Random = UnityEngine.Random;

namespace Game.Features.Dot.Scripts.Systems
{
    public class DotSpawner : IInitializable, IDisposable
    {
        private readonly GridController _gridController;
        private readonly DotFactory _dotFactory;
        private readonly DotSettings _dotSettings;
        private readonly SignalBus _signalBus;

        public DotSpawner(GridController gridController, 
            DotFactory dotFactory, 
            DotSettings dotSettings, 
            SignalBus signalBus)
        {
            _gridController = gridController;
            _dotFactory = dotFactory;
            _dotSettings = dotSettings;
            _signalBus = signalBus;
        }
        
        public void Initialize()
        {
            _signalBus.Subscribe<DotDropCompleteSignal>(PopulateGridWithRandomDots);
        }
        
        public void PopulateGridWithRandomDots()
        {
            while (_gridController.TryGetFreeCellEntity(out var freeCellEntity))
            {
                SpawnRandomDotEntityOnGridCell(freeCellEntity);
            }
        }
        
        public void PopulateGridWithSaveGameData(SaveGameData saveGameData)
        {
            foreach (var data in saveGameData.DotSaveDatas)
            {
                if (_gridController.TryGetGridCellByCoordinate(data.Coordinate, out var gridCellEntity))
                {
                    SpawnDot(gridCellEntity, data.Value);
                }
            }
        }

        private void SpawnRandomDotEntityOnGridCell(GridCellEntity freeCellEntity)
        {
            SpawnDot(freeCellEntity, GetRandomValueForDot());
        }

        private void SpawnDot(GridCellEntity targetGridCell, int value)
        {
            var dotEntity = _dotFactory.Create();
            dotEntity.transform.position = targetGridCell.transform.position;
            dotEntity.Initialize(value, targetGridCell);
        }

        private int GetRandomValueForDot()
        {
            var starterValueList = _dotSettings.StarterValues;
            var randomIndex = Random.Range(0, starterValueList.Count);
            return starterValueList[randomIndex];
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<DotDropCompleteSignal>(PopulateGridWithRandomDots);
        }
    }
}