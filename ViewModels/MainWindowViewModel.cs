using System.Windows.Input;

namespace BattleshipGame.ViewModels
{
    class MainWindowViewModel : ViewModelBase
    {
        private Game? _game;
        public Game Game
        {
            get
            {
                return _game ?? (_game = new Game());
            }
        }
        private OceanGridViewModel? _playerGridViewModel;
        public OceanGridViewModel PlayerGridViewModel
        {
            get
            {
                return _playerGridViewModel ?? (_playerGridViewModel = new OceanGridViewModel(Game.PlayerGrid, true));
            }
        }
        private OceanGridViewModel? _aiGridViewModel;
        public OceanGridViewModel AiGridViewModel
        {
            get
            {
                return _aiGridViewModel ?? (_aiGridViewModel = new OceanGridViewModel(Game.AiGrid, false));
            }
        }

        public MainWindowViewModel()
        {

        }

        private ICommand? _newGameCommand;
        public ICommand NewGameCommand
        {
            get
            {
                return _newGameCommand ?? (_newGameCommand = new CommandHandler(NewGame, () => CanExecute));
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

        public void NewGame()
        {

        }
    }
}
