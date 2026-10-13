using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace memória
{
    public partial class MainWindow : Window
    {
        string[] choosable_numbers = { "1", "2", "3", "4", "5", "6","7","8","9","10","11","12","13","14","15","16","17","18"};
        string[] choosable_colors = { "red", "green", "blue", "cyan", "magenta", "black", "yellow", "white", "grey", "light green", "dark green", "purple", "orange", "pink", "brown", "navy blue", "gold", "silver" };
        string[] choosable_smiles = { "😊", "😂", "🤣", "❤️", "😍", "😒", "🥰", "😘", "😎", "😭", "🥺", "😏", "🤔", "🤩", "🥳", "😡", "💀", "🔥" };

        string keres1 = "";
        int keres1tag = 0;
        string keres2 = "";
        int keres2tag = 0;
        int párok = 0;
        bool gamecheck = false;
        List<Button> cardButtons = new();
        List<string> cards = new();
        int clicks = 0;
        int diff;
        int style; //1 = emoji, 2 = színek, 3 = számok

        public MainWindow()
        {

            InitializeComponent();
            Tb_gamestate.Visibility = Visibility.Collapsed;
            Tb_tries.Visibility = Visibility.Collapsed;
        }

        private void Createcards()
        {
            cards.Clear();

            switch (Lb_Size.SelectedIndex)
            {
                case 0:
                    diff = 2;
                    break;
                case 1:
                    diff = 4;
                    break;
                case 2:
                    diff = 6;
                    break;
                default:
                    diff = 2;
                    break;
            }

            
            switch (Lb_tma.SelectedIndex)
            {
                case 0:
                    style = 1;
                    break; 
                case 1:
                    style = 2;
                    break; 
                case 2:
                    style = 3;
                    break; 
                default:
                    style = 1;
                    break;
            }


            int pairs = diff * diff / 2;

            for (int i = 0; i < pairs; i++)
            {
                string card;

                if (style == 1)
                    card = choosable_smiles[i];
                else if (style == 2)
                    card = choosable_colors[i];
                else
                    card = choosable_numbers[i];

                cards.Add(card);
                cards.Add(card);
            }
        }

           

        private void CreateBtns()
        {
            GameGrid.Children.Clear();
            GameGrid.RowDefinitions.Clear();
            GameGrid.ColumnDefinitions.Clear();
            cardButtons.Clear();

            int columns = diff;
            int rows = diff;

            for (int i = 0; i < rows; i++)
            {
                GameGrid.RowDefinitions.Add(
                    new RowDefinition
                    {
                        Height = new GridLength(1, GridUnitType.Star)
                    });
            }

            for (int i = 0; i < columns; i++)
            {
                GameGrid.ColumnDefinitions.Add(
                    new ColumnDefinition
                    {
                        Width = new GridLength(1, GridUnitType.Star)
                    });
            }

            for (int i = 0; i < cards.Count; i++)
            {
                Button card = new Button
                {
                    Content = "?",
                    Width = 50,
                    FontSize = 28,
                    Margin = new Thickness(5),
                    Background = Brushes.LightBlue,
                    BorderBrush = Brushes.Gray,
                    BorderThickness = new Thickness(2),
                    Tag = i,
                    IsEnabled = true
                };

                card.Click += Card_Click;

                Grid.SetRow(card, i / columns);
                Grid.SetColumn(card, i % columns);

                GameGrid.Children.Add(card);
                cardButtons.Add(card);
            }
        }

        private async void Card_Click(object sender, RoutedEventArgs e)
        {

            if (sender is not Button button || gamecheck)
                return;

            int index = (int)button.Tag;

            if (!button.IsEnabled || !button.IsHitTestVisible)
                return;

            if (style == 2)
            {
                string colorName = (cards[index] ?? string.Empty).Trim().ToLowerInvariant();

                switch (colorName)
                {
                    case "red":
                        button.Background = Brushes.Red;
                        break;
                    case "green":
                        button.Background = Brushes.Green;
                        break;
                    case "blue":
                        button.Background = Brushes.Blue;
                        break;
                    case "cyan":
                        button.Background = Brushes.Cyan;
                        break;
                    case "magenta":
                        button.Background = Brushes.Magenta;
                        break;
                    case "black":
                        button.Background = Brushes.Black;
                        break;
                    case "yellow":
                        button.Background = Brushes.Yellow;
                        break;
                    case "white":
                        button.Background = Brushes.White;
                        break;
                    case "grey":
                    case "gray":
                        button.Background = Brushes.Gray;
                        break;
                    case "light green":
                    case "lightgreen":
                        button.Background = Brushes.LightGreen;
                        break;
                    case "dark green":
                    case "darkgreen":
                        button.Background = Brushes.DarkGreen;
                        break;
                    case "purple":
                        button.Background = Brushes.Purple;
                        break;
                    case "orange":
                        button.Background = Brushes.Orange;
                        break;
                    case "pink":
                        button.Background = Brushes.Pink;
                        break;
                    case "brown":
                        button.Background = Brushes.Brown;
                        break;
                    case "navy blue":
                    case "navyblue":
                    case "navy":
                        button.Background = Brushes.Navy;
                        break;
                    case "gold":
                        button.Background = Brushes.Gold;
                        break;
                    case "silver":
                        button.Background = Brushes.Silver;
                        break;
                    default:
                        button.Background = Brushes.LightBlue;
                        break;
                }

                button.Content = "";
                button.IsHitTestVisible = false;
            }
            else
            {
                button.Content = cards[index];
                button.IsHitTestVisible = false;
            }

            if (keres1 == "")
            {
                keres1 = cards[index];
                keres1tag = index;
                return;
            }

            keres2 = cards[index];
            keres2tag = index;

            gamecheck = true;

            if (keres1 == keres2)
            {
                clicks++;
                Tb_tries.Text = "Próbálkozások száma: " + clicks;

                párok++;

                keres1 = "";
                keres2 = "";

                gamecheck = false;

                if (párok == cards.Count / 2)
                {
                    MessageBox.Show("Gratulálok, megtaláltad az összes párt!");
                    Tb_gamestate.Text = "Játék állása: nyertél 3mp múlva vissza a főmenübe";
                    await Task.Delay(1000);
                    Tb_gamestate.Text = "Játék állása: nyertél 2mp múlva vissza a főmenübe";
                    await Task.Delay(1000);
                    Tb_gamestate.Text = "Játék állása: nyertél 1mp múlva vissza a főmenübe";
                    await Task.Delay(1000);
                    Stck_Main.Visibility = Visibility.Visible;
                    GameGrid.Visibility = Visibility.Collapsed;
                    Tb_gamestate.Visibility = Visibility.Collapsed;
                    Tb_tries.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                await Task.Delay(1000);
                if (style == 2)
                {
                    cardButtons[keres1tag].Content = "?";
                    cardButtons[keres1tag].Background = Brushes.LightBlue;
                    cardButtons[keres1tag].IsHitTestVisible = true;

                    cardButtons[keres2tag].Content = "?";
                    cardButtons[keres2tag].Background = Brushes.LightBlue;
                    cardButtons[keres2tag].IsHitTestVisible = true;
                }
                else
                {
                    cardButtons[keres1tag].Content = "?";
                    cardButtons[keres1tag].IsHitTestVisible = true;

                    cardButtons[keres2tag].Content = "?";
                    cardButtons[keres2tag].IsHitTestVisible = true;
                }
                

                clicks++;
                Tb_tries.Text = "Próbálkozások száma: " + clicks;

                keres1 = "";
                keres2 = "";

                gamecheck = false;
            }
        }



        private void Shuffle()
        {
            Random random = new Random();

            for (int i = cards.Count - 1; i > 0; i--)
            {
                int j = random.Next(i + 1);

                (cards[i], cards[j]) = (cards[j], cards[i]);
            }
        }

        private void Btn_start_Click(object sender, RoutedEventArgs e)
        {
            keres1 = "";
            keres2 = "";
            keres1tag = 0;
            keres2tag = 0;
            párok = 0;
            gamecheck = false;
            clicks = 0;
            Tb_gamestate.Text = "Játék állása: megy";
            Tb_tries.Text = "Próbálkozások száma: " + clicks;

            cards.Clear();
            cardButtons.Clear();
            Createcards();
            Shuffle();
            Stck_Main.Visibility = Visibility.Collapsed;
            GameGrid.Visibility = Visibility.Visible;
            Tb_gamestate.Visibility = Visibility.Visible;
            Tb_tries.Visibility = Visibility.Visible;
            CreateBtns();
        }

    }
}