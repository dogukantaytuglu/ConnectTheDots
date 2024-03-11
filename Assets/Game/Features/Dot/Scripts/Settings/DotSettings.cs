using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Features.Dot.Scripts.Settings
{
    [Serializable]
    public class DotSettings
    {
        public List<Color> DotColorPalette => dotColorPalette;

        [SerializeField] private List<Color> dotColorPalette;
    }
}
