using BattleshipGame.Models;

namespace BattleshipGame.ViewModels
{
    class OceanGridViewModel : ViewModelBase
    {

        private OceanGrid _oceanGrid;
        public bool IsPlayerGrid;

        public OceanGridViewModel(OceanGrid grid, bool isPlayerGrid)
        {
            IsPlayerGrid = isPlayerGrid;
            _oceanGrid = grid;
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
                    result[x].Add(new GridCellViewModel(_oceanGrid, IsPlayerGrid, x, y));
                }
            }

            return result;
        }

    }
}
