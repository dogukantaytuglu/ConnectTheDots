using UnityEngine;

namespace Game.Features.Grid.Scripts.Interfaces
{
    public interface IGridSpaceOccupier
    {
        Vector2 CoordinateOnGrid { get; set; }
    }
}