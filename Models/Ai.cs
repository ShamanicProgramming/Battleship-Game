using BattleshipGame.Converters;
using BattleshipGame.Enums;

namespace BattleshipGame.Models
{
    internal class Ai
    {
        private OceanGrid aiGrid;
        private OceanGrid playerGrid;
        private Random random;

        public Ai(OceanGrid aiGrid, OceanGrid playerGrid)
        {
            random = new Random();
            this.aiGrid = aiGrid;
            this.playerGrid = playerGrid;
        }

        public void GetNextMove(out int x, out int y)
        {
            do
            {
                x = random.Next(0, 10);
                y = random.Next(0, 10);
            } while (playerGrid.IsHitAt(x, y));
        }

        public void CoordsForNextShipPlacement(ShipTypeEnum shipType, out int x1, out int y1, out int x2, out int y2)
        {
            int len = ShipTypeToLengthConverter.GetLengthFromShipType(shipType);
            int cellOffset = len - 1; // The cell offset for checking valid length is one less than the actual length of the ship
            int x;
            int y;
            do
            {
                x = random.Next(0, 10);
                y = random.Next(0, 10);
            } while (aiGrid.IsShipAt(x, y));

            // Randomly select a direction, if it is valid
            int dir = random.Next(0, 4);
            if(aiGrid.AreCoordsValidShipPlacement(shipType, x, x + cellOffset, y, y) && dir == 0)
            {
                x1 = x;
                y1 = y;
                x2 = x + cellOffset;
                y2 = y;
                return;
            }
            else if(aiGrid.AreCoordsValidShipPlacement(shipType, x, x - cellOffset, y, y) && dir == 1)
            {
                x1 = x;
                y1 = y;
                x2 = x - cellOffset;
                y2 = y;
                return;
            }
            else if (aiGrid.AreCoordsValidShipPlacement(shipType, x, x, y, y + cellOffset) && dir == 2)
            {
                x1 = x;
                y1 = y;
                x2 = x;
                y2 = y + cellOffset;
                return;
            }
            else if (aiGrid.AreCoordsValidShipPlacement(shipType,x, x, y, y - cellOffset) && dir == 3)
            {
                x1 = x;
                y1 = y;
                x2 = x;
                y2 = y - cellOffset;
                return;
            }

            // if we didn't find a valid spot to place we can just call the function again
            CoordsForNextShipPlacement(shipType, out x1, out y1, out x2, out y2);
        }
    }
}
