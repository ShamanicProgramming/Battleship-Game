using BattleshipGame.Models;

namespace BattleshipGame
{
    class Game
    {
        private OceanGrid PlayerGrid;
        private OceanGrid AiGrid;
        private bool ShipPlacingPhase;
        private ShipTypeEnum ShipToPlace;

        public Game()
        {
            ShipToPlace = ShipTypeEnum.Carrier;
            PlayerGrid = new OceanGrid();
            AiGrid = new OceanGrid();
            ShipPlacingPhase = true;
        }

        public void SelectedCell(int x1, int x2, int x3, int x4)
        {
            if(ShipPlacingPhase)
            {
                PlayerGrid.PlaceShip(ShipToPlace, x1, x2, x3, x4);
                ShipToPlace = ShipToPlace - 1;
                if(ShipToPlace == ShipTypeEnum.None)
                {
                    ShipPlacingPhase = false;
                }
            }
            else
            {
                 
            }
        }

        public bool IsAiShipAt(int x, int y)
        {
            return AiGrid.Ships[x, y] != ShipTypeEnum.None;
        }

        public bool IsPlayerShipAt(int x, int y)
        {
            return PlayerGrid.Ships[x, y] != ShipTypeEnum.None;
        }

        public bool IsAiHitAt(int x, int y)
        {
            return AiGrid.Hits[x, y];
        }

        public bool IsPlayerHitAt(int x, int y)
        {
            return PlayerGrid.Hits[x, y];
        }
    }
}
