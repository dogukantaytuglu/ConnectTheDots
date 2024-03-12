using Game.Features.Dot.Scripts.Dot;

namespace Game.Features.Dot.Scripts.Signals
{
    public class SelectedDotListChangedSignal
    {
        public readonly DotEntity[] DotEntities;

        public SelectedDotListChangedSignal(DotEntity[] dotEntities)
        {
            DotEntities = dotEntities;
        }
    }
}