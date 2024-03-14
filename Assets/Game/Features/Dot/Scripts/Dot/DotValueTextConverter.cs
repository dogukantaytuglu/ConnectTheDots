namespace Game.Features.Dot.Scripts.Dot
{
    public class DotValueTextConverter
    {
        public string Convert(int value)
        {
            if (value < 1024)
            {
                return value.ToString();
            }

            else
            {
                var dividedToThousand = value / 1000.0;
                return dividedToThousand.ToString("0") + "K";
            }
        }
    }
}