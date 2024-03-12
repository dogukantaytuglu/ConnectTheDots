using System;
using Game.Features.LineBetweenDots.Scripts.Line;
using UnityEngine;

namespace Game.Features.LineBetweenDots.Scripts.Settings
{
    [Serializable]
    public class LineBetweenDotsSettings
    {
        public float LineThickness => lineThickness;

        public Material LineRendererMaterial => lineRendererMaterial;

        public LineBetweenDotsHandler LineRendererGameObject => lineRendererGameObject;

        [SerializeField] private LineBetweenDotsHandler lineRendererGameObject;
        [SerializeField] private Material lineRendererMaterial;
        [SerializeField] private float lineThickness;
    }
}