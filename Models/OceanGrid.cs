using BattleshipGame.Converters;
using BattleshipGame.Enums;
using BattleshipGame.Util;

namespace BattleshipGame.Models
{
    class OceanGrid
    {
        public bool[,] Hits;
        public ShipTypeEnum[,] Ships;
        public Dictionary<ShipTypeEnum, bool> ShipSinkingRecord { get; private set; }
        private MessageHandler messageHandler;

        public OceanGrid(MessageHandler messageHandler)
        {
            this.messageHandler = messageHandler;
            ShipSinkingRecord = new Dictionary<ShipTypeEnum, bool>
            {
                { ShipTypeEnum.Destroyer, false },
                { ShipTypeEnum.Submarine, false },
                { ShipTypeEnum.Cruiser, false },
                { ShipTypeEnum.Carrier, false },
                { ShipTypeEnum.Battleship, false }
            };
            Hits = new bool[10, 10];
            Ships = new ShipTypeEnum[10, 10];
        }

        /// <summary>
        /// Returns true if any ship is detected between the two coords or if the coords are out of bounds.
        /// </summary>
        public bool AreCoordsValidShipPlacement(ShipTypeEnum shipType, int x1, int x2, int y1, int y2)
        {
            if (x1 > 9 || x2 > 9 || y1 > 9 || y2 > 9) return false;
            if (x1 < 0 || x2 < 0 || y1 < 0 || y2 < 0) return false;

            List<int> xRange = UtilStaticMethods.GetNumbersInRange(x1, x2);
            List<int> yRange = UtilStaticMethods.GetNumbersInRange(y1, y2);

            // the range we use is dependant on whether we are placing horizontally or vertically
            if (x1 == x2)
            {
                if (ShipTypeToLengthConverter.GetLengthFromShipType(shipType) != yRange.Count)
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
                if (ShipTypeToLengthConverter.GetLengthFromShipType(shipType) != xRange.Count)
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

        /// <summary>
        /// Records an attempted strike on the grid. Might not necessarily score.
        /// </summary>
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

        /// <summary>
        /// Updates the ShipSinkingRecord for a ship type
        /// Pushes a message if a ship has only just been sunk
        /// </summary>
        private void UpdateShipSunkRecordorShip(ShipTypeEnum shipType)
        {

            if (ShipSinkingRecord[shipType])
            {
                return;
            }

            for (int x = 0; x < 10; x++)
            {
                for (int y = 0; y < 10; y++)
                {
                    if (Ships[x, y] == shipType && !Hits[x, y])
                    {
                        return;
                    }
                }
            }
            ShipSinkingRecord[shipType] = true;
            messageHandler.PushMessage(Enum.GetName(shipType) + " was sunk.");

        }

        /// <summary>
        /// dates the ShipSinkingRecord for all ship types and returns true if all ships have been sunk
        /// </summary>
        /// <returns></returns>
        internal bool CheckAllShipsForSinking()
        {
            foreach (ShipTypeEnum shipType in Enum.GetValues(typeof(ShipTypeEnum)))
            {
                if (shipType == ShipTypeEnum.None)
                {
                    continue;
                }
                UpdateShipSunkRecordorShip(shipType);
            }

            foreach (var rec in ShipSinkingRecord)
            {
                if (rec.Value == false)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
