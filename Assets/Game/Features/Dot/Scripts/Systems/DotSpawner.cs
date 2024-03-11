using Game.Features.Dot.Scripts.Dot;
using Game.Features.Dot.Scripts.Settings;
using Game.Features.Grid.Scripts.Systems;
using UnityEngine;
using Zenject;

namespace Game.Features.Dot.Scripts.Systems
{
    public class DotSpawner: IInitializable
    {
        private readonly GridController _gridController;
        private readonly DotFactory _dotFactory;
        private readonly DotSettings _dotSettings;

        public DotSpawner(GridController gridController, DotFactory dotFactory, DotSettings dotSettings)
        {
            _gridController = gridController;
            _dotFactory = dotFactory;
            _dotSettings = dotSettings;
        }
        
        public void Initialize()
        {
            PopulateGridWithDots();
        }
        
        private void PopulateGridWithDots()
        {
            while (_gridController.TryGetFreeCellEntity(out var freeCellEntity))
            {
                var dotEntity = _dotFactory.Create();
                dotEntity.transform.position = freeCellEntity.transform.position;
                _gridController.RegisterDotToGridCell(freeCellEntity, dotEntity);
                var starterValueList = _dotSettings.StarterValues;
                var randomIndex = Random.Range(0, starterValueList.Count);
                dotEntity.SetValue(starterValueList[randomIndex]);
            }
        }
    }
}