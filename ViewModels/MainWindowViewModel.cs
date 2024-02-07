using BattleshipGame.Util;
using System.Windows.Input;

namespace BattleshipGame.ViewModels
{
    class MainWindowViewModel : ViewModelBase
    {
        public Game Game { get; private set; }

        private OceanGridViewModel? _playerGridViewModel;
        public OceanGridViewModel PlayerGridViewModel
        {
            get
            {
                return _playerGridViewModel ??= new OceanGridViewModel(Game.PlayerGrid, Game, true, RefreshAllSymbols);
            }
        }

        private OceanGridViewModel? _aiGridViewModel;
        public OceanGridViewModel AiGridViewModel
        {
            get
            {
                return _aiGridViewModel ??= new OceanGridViewModel(Game.AiGrid, Game, false, RefreshAllSymbols);
            }
        }

        public MessageHandler MessageHandler;

        public MainWindowViewModel(Action<string> uiMessageAction, Action uiClearMessagesAction)
        {
            MessageHandler = new MessageHandler(uiMessageAction, uiClearMessagesAction);
            Game = new Game(MessageHandler);
            MessageHandler.PushMessage("Welcome to battleship.");
            MessageHandler.PushMessage("Please place your ships. Select the start and end holes.");
            MessageHandler.PushMessage("Placing Carrier (5 holes).");
        }

        public void RefreshAllSymbols()
        {
            AiGridViewModel.RefreshAllSymbols();
            PlayerGridViewModel.RefreshAllSymbols();
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
            MessageHandler.ClearMessages();
            MessageHandler.PushMessage("Welcome to battleship.");
            MessageHandler.PushMessage("Please place your ships. Select the start and end holes.");
            MessageHandler.PushMessage("Placing Carrier (5 holes).");

            Game = new Game(MessageHandler);
            AiGridViewModel.NewGame(Game.AiGrid, Game);
            PlayerGridViewModel.NewGame(Game.PlayerGrid, Game);

            RefreshAllSymbols();
        }
    }
}
