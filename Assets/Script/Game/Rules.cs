namespace TetrisApp
{
    public class Rules
    {
        public static int PointsForClearLines(int clearLines)
        {
            if (clearLines == 1)
            {
                return 1;
            }
            else if (clearLines == 2)
            {
                return 3;
            }
            else if (clearLines == 3)
            {
                return 6;
            }
            else if (clearLines == 4)
            {
                return 10;
            }

            return 1;
        }
    }
}