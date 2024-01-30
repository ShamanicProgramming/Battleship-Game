using System.Windows.Input;

namespace BattleshipGame.ViewModels
{
    class MainWindowViewModel : ViewModelBase
    {
        private Game Game;

        public MainWindowViewModel()
        {
            Game = new Game();
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

        public List<List<char>> AiGrid
        {
            get
            {
                List<List<char>> result = new List<List<char>>();
                for (int x = 0; x < 10; x++)
                {
                    result.Add(new List<char>());
                    for (int y = 0; y < 10; y++)
                    {
                        if (Game.IsAiShipAt(x, y) && Game.IsAiHitAt(x, y))
                        {
                            result[x].Add('X');
                        }
                        else if (Game.IsAiHitAt(x, y))
                        {
                            result[x].Add('O');
                        }
                        else
                        {
                            result[x].Add(' ');
                        }
                    }
                };
                return result;
            }
        }

        public List<List<char>> PlayerGrid
        {
            get
            {
                List<List<char>> result = new List<List<char>>();
                for (int x = 0; x < 10; x++)
                {
                    result.Add(new List<char>());
                    for (int y = 0; y < 10; y++)
                    {
                        if (Game.IsPlayerShipAt(x, y) && Game.IsPlayerHitAt(x, y))
                        {
                            result[x].Add('X');
                        }
                        else if (Game.IsPlayerHitAt(x, y))
                        {
                            result[x].Add('O');
                        }
                        else if (Game.IsPlayerShipAt(x, y))
                        {
                            result[x].Add('█');
                        }
                        else
                        {
                            result[x].Add(' ');
                        }
                    }
                };
                return result;
            }
        }

    }
}
