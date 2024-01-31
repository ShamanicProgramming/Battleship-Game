using BattleshipGame.Models;

namespace BattleshipGame
{
    class Game
    {
        public OceanGrid PlayerGrid { get; private set; }
        public OceanGrid AiGrid { get; private set; }
        private bool shipPlacingPhase;
        private ShipTypeEnum shipToPlace;

        public Game()
        {
            shipToPlace = ShipTypeEnum.Carrier;
            PlayerGrid = new OceanGrid();
            AiGrid = new OceanGrid();
            shipPlacingPhase = false; // TODO should be set to true
        }

        public void SelectCell(int x, int y, bool playerGridClicked)
        {
            if (shipPlacingPhase && playerGridClicked)
            {
            }
            else if (!playerGridClicked && !shipPlacingPhase)
            {
                AiGrid.RecordHit(x, y);
            }
        }

        public void PlaceShipBetweenCoords(int x1, int x2, int x3, int x4)
        {
            PlayerGrid.PlaceShip(shipToPlace, x1, x2, x3, x4);
            shipToPlace = shipToPlace - 1;
            if (shipToPlace == ShipTypeEnum.None)
            {
                shipPlacingPhase = false;
            }
        }

    }
}
