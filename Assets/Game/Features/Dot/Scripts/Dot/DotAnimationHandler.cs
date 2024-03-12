using DG.Tweening;
using Game.Features.Dot.Scripts.Settings;
using UnityEngine;
using Zenject;

namespace Game.Features.Dot.Scripts.Dot
{
    public class DotAnimationHandler : MonoBehaviour
    {
        private DotSettings _dotSettings;

        [Inject]
        public void Construct(DotSettings dotSettings)
        {
            _dotSettings = dotSettings;
        }
        
        public void ScaleUp()
        {
            transform.DOScale(_dotSettings.ScaleAmount ,_dotSettings.ScaleDuration);
        }

        public void ScaleDown()
        {
            transform.DOScale(1f, _dotSettings.ScaleDuration);
        }
    }
}