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

        public DotController(DotSpawner dotSpawner)
        {
            _dotSpawner = dotSpawner;
        }

        public void RegisterDotEntity(DotEntity dotEntity)
        {
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

        public void Initialize()
        {
            _dotSpawner.PopulateGridWithDots();
        }
    }
}