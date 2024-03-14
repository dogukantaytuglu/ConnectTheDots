namespace Game.Features.Dot.Scripts.Dot
{
    public class DotValueTextConverter
    {
        private const int ValueToMod = 1000;

        public string Convert(int value)
        {
            if (value < ValueToMod)
            {
                return value.ToString();
            }

            var dividedToThousand = value / ValueToMod;
            return dividedToThousand.ToString("0") + "K";
        }
    }
}