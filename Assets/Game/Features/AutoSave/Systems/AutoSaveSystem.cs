using System;
using System.Collections.Generic;
using Game.Features.AutoSave.Data;
using Game.Features.Dot.Scripts.Signals;
using Game.Features.Dot.Scripts.Systems;
using UnityEngine;
using Zenject;

namespace Game.Features.AutoSave.Systems
{
    public class AutoSaveSystem : IInitializable, IDisposable
    {
        private readonly SignalBus _signalBus;
        private readonly DotController _dotController;
        private readonly AutoSaveSystem _autoSaveSystem;
        private static string GridSaveDataKey => AutoSaveKeys.GridSaveDataKey;

        public AutoSaveSystem(SignalBus signalBus, DotController dotController)
        {
            _signalBus = signalBus;
            _dotController = dotController;
        }
        
        public void Initialize()
        {
            _signalBus.Subscribe<DotDropCompleteSignal>(SaveGame);
        }

        private void SaveGame()
        {
            var dataList = new List<GridSaveData>();

            var allDotEntities = _dotController.GetAllDotEntities();
            foreach (var dotEntity in allDotEntities)
            {
                var gridSaveData = new GridSaveData(dotEntity.CoordinateOnGrid,
                    dotEntity.Value);
                
                dataList.Add(gridSaveData);
            }

            var saveGameData = new SaveGameData(dataList);

            var json = JsonUtility.ToJson(saveGameData);
            PlayerPrefs.SetString(GridSaveDataKey, json);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<DotDropCompleteSignal>(SaveGame);
        }
    }
}