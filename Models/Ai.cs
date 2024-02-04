using BattleshipGame.Converters;
using BattleshipGame.Enums;
using BattleshipGame.Util;

namespace BattleshipGame.Models
{
    internal class Ai
    {
        private OceanGrid _aiGrid;
        private OceanGrid _playerGrid;
        private Random _random;

        public Ai(OceanGrid aiGrid, OceanGrid playerGrid)
        {
            _random = new Random();
            _aiGrid = aiGrid;
            _playerGrid = playerGrid;
        }

        public void GetNextMove(out int x, out int y)
        {
            do
            {
                x = _random.Next(0, 10);
                y = _random.Next(0, 10);
            } while (_playerGrid.IsHitAt(x, y));
        }

        public void CoordsForNextShipPlacement(ShipTypeEnum shipType, out int x1, out int y1, out int x2, out int y2)
        {
            int len = ShipTypeToLengthConverter.GetLengthFromShipType(shipType);
            int x;
            int y;
            do
            {
                x = _random.Next(0, 10);
                y = _random.Next(0, 10);
            } while (_aiGrid.IsShipAt(x, y));

            if(_aiGrid.AreCoordsValidShipPlacement(shipType, x, x + len, y, y))
            {
                x1 = x;
                y1 = y;
                x2 = x + len;
                y2 = y;
                return;
            }
            else if(_aiGrid.AreCoordsValidShipPlacement(shipType, x, x - len, y, y))
            {
                x1 = x;
                y1 = y;
                x2 = x - len;
                y2 = y;
                return;
            }
            else if (_aiGrid.AreCoordsValidShipPlacement(shipType, x, x, y, y + len))
            {
                x1 = x;
                y1 = y;
                x2 = x;
                y2 = y + len;
                return;
            }
            else if (_aiGrid.AreCoordsValidShipPlacement(shipType,x, x, y, y - len))
            {
                x1 = x;
                y1 = y;
                x2 = x;
                y2 = y - len;
                return;
            }

            // if we didn't find a valid spot to place we can just call the function again
            CoordsForNextShipPlacement(shipType, out x1, out y1, out x2, out y2);
        }
    }
}
