using Game.Features.AutoSave.Data;
using UnityEngine;

namespace Game.Features.AutoSave.Systems
{
    public class LoadGameSystem
    {
        private static string GridSaveDataKey => AutoSaveKeys.GridSaveDataKey;

        public bool TryLoadGame(out SaveGameData saveGameData)
        {
            saveGameData = null;
            if (!PlayerPrefs.HasKey(GridSaveDataKey)) return false;

            var json = PlayerPrefs.GetString(GridSaveDataKey);
            saveGameData = JsonUtility.FromJson<SaveGameData>(json);
            return true;
        }
    }
}