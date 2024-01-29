using System.Windows;
using System.Windows.Controls;

namespace BattleshipGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        readonly static string[] columnMarkers = [" ", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J"];

        public MainWindow()
        {
            InitializeComponent();

            DrawGridIndexMarkers();

            DrawGridButtons();
        }

        private void DrawGridButtons()
        {
            for (int x = 1; x < 11; x++)
            {
                for (int y = 1; y < 11; y++)
                {
                    Button button = CreateStandardGridButton();
                    button.Name = columnMarkers[y] + x.ToString() + "Player";
                    Grid.SetColumn(button, x);
                    Grid.SetRow(button, y);
                    PlayerGrid.Children.Add(button);

                    button = CreateStandardGridButton();
                    button.Name = columnMarkers[y] + x.ToString() + "Ai";
                    Grid.SetColumn(button, x);
                    Grid.SetRow(button, y);
                    AiGrid.Children.Add(button);
                }
            }
        }

        private static Button CreateStandardGridButton()
        {
            Button button = new Button();
            button.Height = 50;
            button.Width = 50;
            button.FontSize = 23;
            button.HorizontalContentAlignment = HorizontalAlignment.Center;
            button.VerticalContentAlignment = VerticalAlignment.Center;
            return button;
        }

        private void DrawGridIndexMarkers()
        {
            // rows
            for (int i = 1; i < 11; i++)
            {
                // player grid
                TextBox box = CreateStandardCell();
                Grid.SetColumn(box, i);
                Grid.SetRow(box, 0);
                box.Text = i.ToString();
                PlayerGrid.Children.Add(box);

                // ai grid
                box = CreateStandardCell();
                Grid.SetColumn(box, i);
                Grid.SetRow(box, 0);
                box.Text = i.ToString();
                AiGrid.Children.Add(box);

            }

            // columns
            for (int i = 1; i < 11; i++)
            {
                // player grid
                TextBox box = CreateStandardCell();
                Grid.SetColumn(box, 0);
                Grid.SetRow(box, i);
                box.Text = columnMarkers[i];
                PlayerGrid.Children.Add(box);

                // ai grid
                box = CreateStandardCell();
                Grid.SetColumn(box, 0);
                Grid.SetRow(box, i);
                box.Text = columnMarkers[i];
                AiGrid.Children.Add(box);
            }
        }

        private void NewGame(object sender, RoutedEventArgs e)
        {

        }

        private static TextBox CreateStandardCell()
        {
            TextBox box = new TextBox();
            box.IsReadOnly = true;
            box.Height = 50;
            box.Width = 50;
            box.FontSize = 23;
            box.TextAlignment = TextAlignment.Center;
            box.VerticalContentAlignment = VerticalAlignment.Center;

            box.BorderThickness = new Thickness(1, 1, 1, 1);
            return box;
        }
    }
}