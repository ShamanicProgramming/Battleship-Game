using BattleshipGame.Models;

namespace BattleshipGame
{
    class Game
    {
        public OceanGrid PlayerGrid;
        public OceanGrid AiGrid;
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
            if (ShipPlacingPhase)
            {
                PlayerGrid.PlaceShip(ShipToPlace, x1, x2, x3, x4);
                ShipToPlace = ShipToPlace - 1;
                if (ShipToPlace == ShipTypeEnum.None)
                {
                    ShipPlacingPhase = false;
                }
            }
            else
            {

            }
        }

    }
}
