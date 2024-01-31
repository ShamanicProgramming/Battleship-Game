using BattleshipGame.Models;
using System.Windows;
using System.Windows.Input;

namespace BattleshipGame.ViewModels
{
    class GridCellViewModel : ViewModelBase
    {
        private OceanGrid _oceanGrid;
        private readonly int x;
        private readonly int y;
        private readonly bool isPlayerCell;

        public GridCellViewModel(OceanGrid oceanGrid, bool isPlayerCell, int x, int y)
        {
            _oceanGrid = oceanGrid;
            this.isPlayerCell = isPlayerCell;
            this.x = x;
            this.y = y;
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
            MessageBox.Show("Yay");
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
