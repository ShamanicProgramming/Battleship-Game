namespace BattleshipGame.Util
{
    internal static class UtilStaticMethods
    {
        public static List<int> GetNumbersInRange(int start, int end)
        {
            List<int> result = new List<int>();

            // Ensure start is less than or equal to end
            if (start > end)
            {
                int temp = start;
                start = end;
                end = temp;
            }

            for (int i = start; i <= end; i++)
            {
                result.Add(i);
            }

            return result;
        }
    }
}
