using System;
using System.Collections.Generic;
using Game.Features.Dot.Scripts.Dot;
using UnityEngine;

namespace Game.Features.Dot.Scripts.Settings
{
    [Serializable]
    public class DotSettings
    {
        public List<Color> DotColorPalette => dotColorPalette;
        public DotEntity DotPrefab => dotPrefab;

        [SerializeField] private List<Color> dotColorPalette;
        [SerializeField] private DotEntity dotPrefab;
    }
}
