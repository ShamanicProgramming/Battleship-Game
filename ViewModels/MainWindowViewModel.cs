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
                return _game ??= new Game();
            }
        }

        private OceanGridViewModel? _playerGridViewModel;
        public OceanGridViewModel PlayerGridViewModel
        {
            get
            {
                return _playerGridViewModel ??= new OceanGridViewModel(Game.PlayerGrid, Game, true, MessageHandler);
            }
        }

        private OceanGridViewModel? _aiGridViewModel;
        public OceanGridViewModel AiGridViewModel
        {
            get
            {
                return _aiGridViewModel ??= new OceanGridViewModel(Game.AiGrid, Game, false, MessageHandler);
            }
        }

        public MessageHandler MessageHandler;

        public MainWindowViewModel(Action<string> uiMessageAction)
        {
            MessageHandler = new MessageHandler(uiMessageAction);
            MessageHandler.PushMessage("Welcome to battleship.");
            MessageHandler.PushMessage("Please place your ships. Select the start and end holes.");
            MessageHandler.PushMessage("Placing Carrier (5 holes).");
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
