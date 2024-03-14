using System;
using System.Collections.Generic;
using Game.Features.AutoSave.Data;
using Game.Features.Dot.Scripts.Signals;
using Game.Features.Grid.Scripts.Systems;
using UnityEngine;
using Zenject;

namespace Game.Features.AutoSave.Systems
{
    public class AutoSaveSystem : IInitializable, IDisposable
    {
        private readonly string SaveGameKey = "SAVE_GAME_DATA";
        private readonly SignalBus _signalBus;
        private readonly GridController _gridController;
        private readonly AutoSaveSystem _autoSaveSystem;

        public AutoSaveSystem(SignalBus signalBus, GridController gridController)
        {
            _signalBus = signalBus;
            _gridController = gridController;
        }
        
        public void Initialize()
        {
            _signalBus.Subscribe<DotDropCompleteSignal>(SaveGame);
        }

        private void SaveGame()
        {
            var dataList = new List<GridSaveData>();
            
            foreach (var gridCellEntity in _gridController.AllGridCells)
            {
                var gridSaveData = new GridSaveData(gridCellEntity.GridCoordinates,
                    gridCellEntity.RegisteredDotEntity.Value);
                
                dataList.Add(gridSaveData);
            }

            var saveGameData = new SaveGameData(dataList);

            var json = JsonUtility.ToJson(saveGameData);
            PlayerPrefs.SetString(SaveGameKey, json);
        }

        public bool TryLoadGame(out SaveGameData saveGameData)
        {
            saveGameData = null;
            if (!PlayerPrefs.HasKey(SaveGameKey)) return false;

            var json = PlayerPrefs.GetString(SaveGameKey);
            saveGameData = JsonUtility.FromJson<SaveGameData>(json);
            return true;
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<DotDropCompleteSignal>(SaveGame);
        }
    }
}