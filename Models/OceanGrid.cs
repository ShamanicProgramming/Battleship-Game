using System.Windows.Controls;

namespace BattleshipGame.Models
{
    class OceanGrid
    {
        public bool[,] Hits;
        public ShipTypeEnum[,] Ships;

        public OceanGrid()
        {
            Hits = new bool[10, 10];
            Ships = new ShipTypeEnum[10, 10];
        }

        public void PlaceShip(ShipTypeEnum shipType, int x1, int x2, int y1, int y2)
        {
            List<int> xRange = GetNumbersInRange(y1, y2);
            List<int> yRange = GetNumbersInRange(x1, x2);

            for (int i = 0; i < xRange.Count; i++)
            {
                Ships[xRange[i], yRange[i]] = shipType;
            }
        }

        public void RecordHit(int x, int y)
        {
            Hits[x, y] = true;
        }

        static List<int> GetNumbersInRange(int start, int end)
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

        public void ResetShips()
        {
            Hits = new bool[10, 10];
            Ships = new ShipTypeEnum[10, 10];
        }

        public bool IsShipAt(int x, int y)
        {
            return Ships[x, y] != ShipTypeEnum.None;
        }

        public bool IsHitAt(int x, int y)
        {
            return Hits[x, y];
        }
    }
}
