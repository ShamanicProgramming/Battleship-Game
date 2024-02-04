using BattleshipGame.Enums;

namespace BattleshipGame.Converters
{
    internal static class ShipTypeToLengthConverter
    {
        public static int GetLengthFromShipType(ShipTypeEnum type)
        {
            switch (type)
            {
                case ShipTypeEnum.None:
                    return 0;
                case ShipTypeEnum.Destroyer:
                    return 2;
                case ShipTypeEnum.Submarine:
                    return 3;
                case ShipTypeEnum.Cruiser:
                    return 3;
                case ShipTypeEnum.Battleship:
                    return 4;
                case ShipTypeEnum.Carrier:
                    return 5;
            }
            throw new ArgumentOutOfRangeException();
        }

    }
}
