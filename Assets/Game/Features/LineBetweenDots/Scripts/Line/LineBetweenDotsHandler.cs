using System;
using Game.Features.Dot.Scripts.Signals;
using UnityEngine;
using Zenject;

namespace Game.Features.LineBetweenDots.Scripts.Line
{
    public class LineBetweenDotsHandler : MonoBehaviour, IDisposable
    {
        [SerializeField] private LineRenderer lineRenderer;
        
        private SignalBus _signalBus;
        private Material _material;

        [Inject]
        public void Construct(SignalBus signalBus, Material lineMaterial)
        {
            _signalBus = signalBus;
            _material = lineMaterial;
            lineRenderer.material = lineMaterial;
        }

        private void Awake()
        {
            _signalBus.Subscribe<SelectedDotListChangedSignal>(HandleSelectedDotsChanged);
            _signalBus.Subscribe<FirstDotSelectedSignal>(SetLineColor);
        }

        private void SetLineColor(FirstDotSelectedSignal signal)
        {
            var color = signal.Color;
            _material.color = color;
        }

        private void HandleSelectedDotsChanged(SelectedDotListChangedSignal signal)
        {
            var dotEntities = signal.DotEntities;
            if (dotEntities.Length < 1)
            {
                lineRenderer.positionCount = 0;
                return;
            }

            lineRenderer.positionCount = dotEntities.Length;
            for (var i = 0; i < dotEntities.Length; i++)
            {
                var dotEntity = dotEntities[i];
                lineRenderer.SetPosition(i, dotEntity.LineBetweenDotsRoutePoint);
            }
    
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<SelectedDotListChangedSignal>(HandleSelectedDotsChanged);
            _signalBus.Unsubscribe<FirstDotSelectedSignal>(SetLineColor);
        }
    }
}