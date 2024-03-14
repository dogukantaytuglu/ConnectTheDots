using System;
using System.Collections.Generic;

namespace Game.Features.AutoSave.Data
{
    [Serializable]  
    public class SaveGameData
    {
        public List<GridSaveData> DotSaveDatas;

        public SaveGameData(List<GridSaveData> dotSaveDatas)
        {
            DotSaveDatas = dotSaveDatas;
        }
    }
}