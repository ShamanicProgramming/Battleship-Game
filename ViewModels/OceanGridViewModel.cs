using BattleshipGame.Enums;
using BattleshipGame.Models;

namespace BattleshipGame.ViewModels
{
    class OceanGridViewModel : ViewModelBase
    {

        private OceanGrid _oceanGrid;
        private Game _game;
        public bool IsPlayerGrid;
        private Action refreshAllSymbols;

        public OceanGridViewModel(OceanGrid grid, Game game, bool isPlayerGrid, Action refreshAllSymbols)
        {
            IsPlayerGrid = isPlayerGrid;
            _oceanGrid = grid;
            _game = game;
            this.refreshAllSymbols = refreshAllSymbols;
        }

        public string CarrierStatusText
        {
            get
            {
                if (_oceanGrid.ShipSinkingRecord[ShipTypeEnum.Carrier])
                {
                    return "Carrier - Sunk";
                }
                else
                {
                    return "Carrier - OK";
                }
            }
        }

        public string BattleshipStatusText
        {
            get
            {
                if(_oceanGrid.ShipSinkingRecord[ShipTypeEnum.Battleship])
                {
                    return "Battleship - Sunk";
                }
                else
                {
                    return "Battleship - OK";
                }
            }
        }

        public string SubmarineStatusText
        {
            get
            {
                if (_oceanGrid.ShipSinkingRecord[ShipTypeEnum.Submarine])
                {
                    return "Submarine - Sunk";
                }
                else
                {
                    return "Submarine - OK";
                }
            }
        }

        public string CruiserStatusText
        {
            get
            {
                if (_oceanGrid.ShipSinkingRecord[ShipTypeEnum.Cruiser])
                {
                    return "Cruiser - Sunk";
                }
                else
                {
                    return "Cruiser - OK";
                }
            }
        }

        public string DestroyerStatusText
        {
            get
            {
                if (_oceanGrid.ShipSinkingRecord[ShipTypeEnum.Destroyer])
                {
                    return "Destroyer - Sunk";
                }
                else
                {
                    return "Destroyer - OK";
                }
            }
        }

        private List<List<GridCellViewModel>>? _oceanGridCells;
        public List<List<GridCellViewModel>> OceanGridCells
        {
            get
            {
                return _oceanGridCells ?? (_oceanGridCells = CreateOceanGridCells());
            }
        }

        private List<List<GridCellViewModel>> CreateOceanGridCells()
        {
            List<List<GridCellViewModel>> result = new List<List<GridCellViewModel>>();
            for (int x = 0; x < 10; x++)
            {
                result.Add(new List<GridCellViewModel>());
                for (int y = 0; y < 10; y++)
                {
                    result[x].Add(new GridCellViewModel(_oceanGrid, _game, IsPlayerGrid, x, y, refreshAllSymbols));
                }
            }

            return result;
        }

        internal void RefreshAllSymbols()
        {
            for (int x = 0; x < 10; x++)
            {
                for (int y = 0; y < 10; y++)
                {
                    OceanGridCells[x][y].RefreshSymbol();
                }
            }
            OnPropertyChanged(nameof(CarrierStatusText));
            OnPropertyChanged(nameof(BattleshipStatusText));
            OnPropertyChanged(nameof(CruiserStatusText));
            OnPropertyChanged(nameof(SubmarineStatusText));
            OnPropertyChanged(nameof(DestroyerStatusText));
        }

        internal void NewGame(OceanGrid oceanGrid, Game game)
        {
            _oceanGrid = oceanGrid;
            _game = game;

            for (int x = 0; x < 10; x++)
            {
                for (int y = 0; y < 10; y++)
                {
                    OceanGridCells[x][y].NewGame(oceanGrid, game);
                }
            }
        }
    }
}
