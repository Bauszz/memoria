using System.Text;
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
    /// <summary>
    ///  1.) középen menü -> title, local highscores (time, tries?), continue game (saves gamestate after getting a card right?), new game
    ///  2.) new game -> difficulty slider, név bekérdezés (default = player)
    ///  3.) create game field difficulty * 2, dynamic card sizes, show them face up for a sec
    ///  4.) all cards go to the middle then back in formation * 3
    ///  5.) game start, clickable buttons, timer starts
    ///  6.) game end, congratulate the player and return to main window within 5 sec (countdown), save to file.
    /// </summary>
    public partial class MainWindow : Window
    {
        string[] choosable_numbers = ["1", "2", "3", "4", "5", "6"];
        string[] choosable_colors = ["r","g","b","c","m","bg","white"];
        string[] choosable_smiles = ["😊","😂","🤣","❤️","😍","😒"];
        string[] cards = [];
        string playername = "";
        public MainWindow()
        {
            InitializeComponent();
            Mainmenu();
            Shuffle();
        }

        private void Mainmenu() 
        {
            var newgame = new Button
            {
                Content = "Új játék",
                Width = 120,
                Height = 40
            };

            var conti = new Button
            {
                Content = $"Játék folytatása: ",
                Width = 120,
                Height = 40,

            };

            Grid.Children.Add(newgame);
            Grid.Children.Add(conti);
            
        }
        private void Createfield()
        {
        
        }

        private void Drawcards()
        {
        
        }
        private void Shuffle()
        {

        }
    }
}