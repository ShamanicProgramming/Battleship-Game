using BattleshipGame.Converters;
using BattleshipGame.Enums;
using BattleshipGame.Models;
using BattleshipGame.Util;

namespace BattleshipGame
{
    class Game
    {
        public OceanGrid PlayerGrid { get; private set; }
        public OceanGrid AiGrid { get; private set; }
        private bool shipPlacingPhase;
        private ShipTypeEnum shipToPlace;
        private Ai ai;
        private MessageHandler messageHandler;
        public bool GameFinished { get; private set; }

        public Game(MessageHandler messageHandler)
        {
            this.messageHandler = messageHandler;
            shipToPlace = ShipTypeEnum.Carrier;
            PlayerGrid = new OceanGrid(messageHandler);
            AiGrid = new OceanGrid(messageHandler);
            shipPlacingPhase = true;
            ai = new Ai(AiGrid, PlayerGrid);
            GameFinished = false;
        }

        private int previousX;
        private int previousY;
        private bool shipStartAlreadySelected;
        /// <summary>
        /// No action if coords are invalid
        /// </summary>
        public void PlayerAction(int x, int y, bool playerGridSelected)
        {
            if (shipPlacingPhase && playerGridSelected)
            {
                if(shipStartAlreadySelected)
                {
                    if(PlayerGrid.AreCoordsValidShipPlacement(shipToPlace, x, previousX, y, previousY))
                    {
                        PlayerGrid.PlaceShip(shipToPlace, x, previousX, y, previousY);
                        messageHandler.PushMessage("Ship Placed.");
                        AiTurn();
                        shipStartAlreadySelected = false;
                        shipToPlace = shipToPlace - 1;

                        switch (shipToPlace)
                        {
                            case ShipTypeEnum.None:
                                messageHandler.PushMessage("Finished placing ships.");
                                break;
                            case ShipTypeEnum.Carrier:
                                messageHandler.PushMessage("Placing Carrier (5 holes).");
                                break;
                            case ShipTypeEnum.Battleship:
                                messageHandler.PushMessage("Placing Battleship (4 holes).");
                                break;
                            case ShipTypeEnum.Cruiser:
                                messageHandler.PushMessage("Placing Cruiser (3 holes).");
                                break;
                            case ShipTypeEnum.Submarine:
                                messageHandler.PushMessage("Placing Submarine (3 holes).");
                                break;
                            case ShipTypeEnum.Destroyer:
                                messageHandler.PushMessage("Placing Destroyer (2 holes).");
                                break;
                        }
                    }
                    else
                    {
                        messageHandler.PushMessage("Selection invalid for complete ship.");
                        messageHandler.PushMessage("Player now placing a ship starting at " + GridXToLetterConverter.GridXToLetter(x) + (y + 1) + ".");
                    }
                }
                else
                {
                    messageHandler.PushMessage("Player is placing a ship starting at " + GridXToLetterConverter.GridXToLetter(x) + (y + 1) + ".");
                    shipStartAlreadySelected = true;
                }
            }
            else if (!playerGridSelected && !shipPlacingPhase)
            {
                messageHandler.PushMessage("Player fired at " + GridXToLetterConverter.GridXToLetter(x) + (y + 1) + ".");
                AiGrid.RecordHit(x, y);
                if(AiGrid.IsShipAt(x, y))
                {
                    messageHandler.PushMessage("It's a hit!");
                    if(AiGrid.CheckAllShipsForSinking())
                    {
                        GameFinished = true;
                        messageHandler.PushMessage("Game over. Player has won!");
                        return;
                    }
                }
                AiTurn();
            }
            
            if (shipToPlace == ShipTypeEnum.None)
            {
                shipPlacingPhase = false;
            }
            previousX = x;
            previousY = y;
    }

        public void AiTurn()
        {
            if (shipPlacingPhase)
            {
                ai.CoordsForNextShipPlacement(shipToPlace, out int x1, out int y1, out int x2, out int y2);
                AiGrid.PlaceShip(shipToPlace, x1, x2, y1, y2);
            }
            else
            {
                ai.GetNextMove(out int x, out int y);
                messageHandler.PushMessage("Ai fired at " + GridXToLetterConverter.GridXToLetter(x) + (y + 1) + ".");
                PlayerGrid.RecordHit(x, y);
                if (PlayerGrid.IsShipAt(x, y))
                {
                    messageHandler.PushMessage("It's a hit!");
                    if(PlayerGrid.CheckAllShipsForSinking())
                    {
                        GameFinished = true;
                        messageHandler.PushMessage("Game over. Ai has won!");
                    }
                }
            }
        }

        internal void NewGame()
        {
            throw new NotImplementedException();
        }
    }
}
