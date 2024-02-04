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
        }
    }
}
