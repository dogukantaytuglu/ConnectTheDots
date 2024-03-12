using System.Collections.Generic;
using Game.Features.Dot.Scripts.Dot;
using UnityEngine;
using Zenject;

namespace Game.Features.Dot.Scripts.Systems
{
    public class DotController : IInitializable
    {
        private readonly DotSpawner _dotSpawner;

        private readonly Dictionary<Transform, DotEntity> _dotEntityTransformDictionary = new();
        private readonly HashSet<DotEntity> _allDotEntities = new();

        public DotController(DotSpawner dotSpawner)
        {
            _dotSpawner = dotSpawner;
        }
        
        public void Initialize()
        {
            _dotSpawner.PopulateGridWithDots();
        }

        public void RegisterDotEntity(DotEntity dotEntity)
        {
            if (!_allDotEntities.Add(dotEntity))
            {
                this.LogError($"{dotEntity.name} is trying to register itself multiple times");
                return;
            }
            
            _dotEntityTransformDictionary.Add(dotEntity.transform, dotEntity);
        }

        public bool TryGetDotEntity(Transform dotEntityTransform, out DotEntity dotEntity)
        {
            dotEntity = null;
            
            if (_dotEntityTransformDictionary.TryGetValue(dotEntityTransform, out var value))
            {
                dotEntity = value;
                return true;
            }

            return false;
        }
        
        public bool IsNeighbourDot(DotEntity originDot, DotEntity dotToCheck)
        {
            var originCoordinate = originDot.DotCoordinate;
            var coordinateToCheck = dotToCheck.DotCoordinate;

            var deltaX = Mathf.Abs(originCoordinate.x - coordinateToCheck.x);
            var deltaY = Mathf.Abs(originCoordinate.y - coordinateToCheck.y);

            return deltaX <= 1 && deltaY <= 1;
        }
    }
}