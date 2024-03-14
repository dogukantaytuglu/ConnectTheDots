using System;
using UnityEngine;

namespace Game.Features.AutoSave.Data
{
    [Serializable]
    public class GridSaveData
    {
        public Vector2 Coordinate;
        public int Value;

        public GridSaveData(Vector2 coordinate, int value)
        {
            Coordinate = coordinate;
            Value = value;
        }
    }
}