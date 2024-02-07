using BattleshipGame.ViewModels;
using System.Windows;

namespace BattleshipGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel((message) => { MessageLog.AppendText(message); MessageLog.ScrollToEnd(); }, MessageLog.Clear);
        }

    }
}