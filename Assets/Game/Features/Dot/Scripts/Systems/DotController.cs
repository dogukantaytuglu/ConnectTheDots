using System.Collections.Generic;
using Game.Features.AutoSave.Systems;
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
        private readonly LoadGameSystem _loadGameSystem;

        public DotController(DotSpawner dotSpawner, LoadGameSystem loadGameSystem)
        {
            _dotSpawner = dotSpawner;
            _loadGameSystem = loadGameSystem;
        }
        
        public void Initialize()
        {
            if (_loadGameSystem.TryLoadGame(out var saveGameData))
            {
                _dotSpawner.PopulateGridWithSaveGameData(saveGameData);
            }
            else
            {
                _dotSpawner.PopulateGridWithRandomDots();
            }
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

        public void DeregisterDotEntity(DotEntity dotEntity)
        {
            if (!_allDotEntities.Remove(dotEntity))
            {
                this.LogError($"{dotEntity.name} is trying to de-register itself multiple times");
                return;
            }

            _dotEntityTransformDictionary.Remove(dotEntity.transform);
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
            var originCoordinate = originDot.CoordinateOnGrid;
            var coordinateToCheck = dotToCheck.CoordinateOnGrid;

            var deltaX = Mathf.Abs(originCoordinate.x - coordinateToCheck.x);
            var deltaY = Mathf.Abs(originCoordinate.y - coordinateToCheck.y);

            return deltaX <= 1 && deltaY <= 1;
        }

        public DotEntity[] GetAllDotEntities()
        {
            var allEntitiesArray = new DotEntity[_allDotEntities.Count];
            var index = 0;
            foreach (var dotEntity in _allDotEntities)
            {
                allEntitiesArray[index] = dotEntity;
                index++;
            }

            return allEntitiesArray;
        }
    }
}