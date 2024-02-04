using BattleshipGame.Models;
using System.Windows.Input;

namespace BattleshipGame.ViewModels
{
    class GridCellViewModel : ViewModelBase
    {
        private OceanGrid _oceanGrid;
        private Game _game;
        private readonly int x;
        private readonly int y;
        private readonly bool isPlayerCell;
        private Action refreshAllSymbols;

        public GridCellViewModel(OceanGrid oceanGrid, Game game, bool isPlayerCell, int x, int y, Action refreshAllSymbols)
        {
            _oceanGrid = oceanGrid;
            this.isPlayerCell = isPlayerCell;
            this.x = x;
            this.y = y;
            _game = game;
            this.refreshAllSymbols = refreshAllSymbols;
        }

        public char Symbol
        {
            get
            {
                if (_oceanGrid.IsShipAt(x, y) && _oceanGrid.IsHitAt(x, y))
                {
                    return 'X';
                }
                else if (_oceanGrid.IsHitAt(x, y))
                {
                    return 'O';
                }
                else if (_oceanGrid.IsShipAt(x, y) && isPlayerCell)
                {
                    return '█';
                }
                else
                {
                    return ' ';
                }
            }
        }

        public void TargetClicked()
        {
            _game.PlayerAction(x, y, isPlayerCell);
            refreshAllSymbols.Invoke();
        }

        internal void RefreshSymbol()
        {
            OnPropertyChanged(nameof(Symbol));
        }

        private ICommand? _targetClickedCommand;
        public ICommand TargetClickedCommand
        {
            get
            {
                return _targetClickedCommand ?? (_targetClickedCommand = new CommandHandler(TargetClicked, () => CanExecute));
            }
        }

        public bool CanExecute
        {
            get
            {
                // check if executing is allowed, i.e., validate, check if a process is running, etc. 
                return true;
            }
        }
    }
}
