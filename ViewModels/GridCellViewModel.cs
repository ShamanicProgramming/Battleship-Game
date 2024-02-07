using BattleshipGame.Models;
using System.Windows.Input;

namespace BattleshipGame.ViewModels
{
    class GridCellViewModel : ViewModelBase
    {
        private OceanGrid oceanGrid;
        private Game game;
        private readonly int x;
        private readonly int y;
        private readonly bool isPlayerCell;
        private Action refreshAllSymbols;

        public GridCellViewModel(OceanGrid oceanGrid, Game game, bool isPlayerCell, int x, int y, Action refreshAllSymbols)
        {
            this.oceanGrid = oceanGrid;
            this.isPlayerCell = isPlayerCell;
            this.x = x;
            this.y = y;
            this.game = game;
            this.refreshAllSymbols = refreshAllSymbols;
        }

        public char Symbol
        {
            get
            {
                if (oceanGrid.IsShipAt(x, y) && oceanGrid.IsHitAt(x, y))
                {
                    return 'X';
                }
                else if (oceanGrid.IsHitAt(x, y))
                {
                    return 'O';
                }
                else if (oceanGrid.IsShipAt(x, y) && isPlayerCell)
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
            game.PlayerAction(x, y, isPlayerCell);
            refreshAllSymbols.Invoke();
        }

        internal void RefreshSymbol()
        {
            OnPropertyChanged(nameof(Symbol));
        }

        internal void NewGame(OceanGrid oceanGrid, Game game)
        {
            this.oceanGrid = oceanGrid;
            this.game = game;
        }

        private ICommand? _targetClickedCommand;
        public ICommand TargetClickedCommand
        {
            get
            {
                return _targetClickedCommand ?? (_targetClickedCommand = new CommandHandler(TargetClicked, () => CanExecuteGridCellButton));
            }
        }

        public bool CanExecuteGridCellButton
        {
            get
            {
                return !game.GameFinished;
            }
        }
    }
}
