using BattleshipGame.Converters;
using BattleshipGame.Enums;
using BattleshipGame.Util;

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

        /// <summary>
        /// Returns true if any ship is detected between the two coords or if the coords are out of bounds.
        /// </summary>
        public bool AreCoordsValidShipPlacement(ShipTypeEnum shipType, int x1, int x2, int y1, int y2)
        {
            if(x1 > 10 || x2 > 10 || y1 > 10 || y2 > 10) return false;
            if(x1 < 0 || x2 < 0 || y1 < 0 || y2 < 0) return false;

            List<int> xRange = UtilStaticMethods.GetNumbersInRange(x1, x2);
            List<int> yRange = UtilStaticMethods.GetNumbersInRange(y1, y2);

            // the range we use is dependant on whether we are placing horizontally or vertically
            if (x1 == x2)
            {
                if(ShipTypeToLengthConverter.GetLengthFromShipType(shipType) > yRange.Count)
                {
                    return false;
                }
                for (int i = 0; i < yRange.Count; i++)
                {
                    if (IsShipAt(x1, yRange[i]))
                    {
                        return false;
                    }
                }
            }
            else if (y1 == y2)
            {
                if (ShipTypeToLengthConverter.GetLengthFromShipType(shipType) > xRange.Count)
                {
                    return false;
                }
                for (int i = 0; i < xRange.Count; i++)
                {
                    if (IsShipAt(xRange[i], y1))
                    {
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }

            return true;
        }

        public void PlaceShip(ShipTypeEnum shipType, int x1, int x2, int y1, int y2)
        {
            List<int> xRange = UtilStaticMethods.GetNumbersInRange(x1, x2);
            List<int> yRange = UtilStaticMethods.GetNumbersInRange(y1, y2);

            // the range we use is dependant on whether we are placing horizontally or vertically
            if (x1 == x2)
            {
                for (int i = 0; i < yRange.Count; i++)
                {
                    Ships[x1, yRange[i]] = shipType;
                }
            }
            else if (y1 == y2)
            {
                for (int i = 0; i < xRange.Count; i++)
                {
                    Ships[xRange[i], y1] = shipType;
                }
            }
            
        }

        public void RecordHit(int x, int y)
        {
            Hits[x, y] = true;
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

        /// <summary>
        /// Returns true if a hit has been attempted at the coordinates
        /// </summary>
        public bool IsHitAt(int x, int y)
        {
            return Hits[x, y];
        }
    }
}
