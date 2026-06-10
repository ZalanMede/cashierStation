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
using SQLite;
using cashierStation_zaroProject.Models;
using cashierStation_zaroProject.Services;

namespace cashierStation_zaroProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            using (SQLiteConnection conn = new SQLiteConnection(App.dataBasePath))
            {
                try
                {
                    List<User> usersList = new List<User>
                    {
                        new User("User1", "User Neve", 3, PassManager.HashPassword("Pass1")),
                        new User("admin", "Admin Teljesnev", 0, PassManager.HashPassword("admin"))
                    };
                    var felhasznaloRepo = new GenericRepository<User>(App.dataBasePath);
                    felhasznaloRepo.InsertMultiple(usersList);

                    var itemRepo = new GenericRepository<Item>(App.dataBasePath);
                    List<Item> itemsList = new List<Item> {
                        new Item("Tojás", 95.00, 27.37, "tojas", "Maris néni", "Toás :D (igen, egy darab)"),
                        new Item("Tej", 250.00, 27.37, "tej", "Maris néni", "Tej."),
                        new Item("Kenyér", 400.00, 27.37, "kenyer", "Maris néni", "Kenyér, talán kovászos"),
                        new Item("Sajt", 500.00, 27.37, "sajt", "Maris néni", "Sajt, nem büdös"),
                        new Item("Alma", 150.00, 27.37, "alma", "Maris néni", "Piros alma"),
                    };
                    itemRepo.InsertMultiple(itemsList);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
        }

        private void loginBtn_Click(object sender, RoutedEventArgs e)
        {
            string felhasznaloNevInput = loginUserInTxt.Text;
            string jelszoInput = PassManager.HashPassword(loginPasswordInTxt.Password);

            if (!string.IsNullOrEmpty(loginUserInTxt.Text) && !string.IsNullOrEmpty(loginPasswordInTxt.Password))
            {
                using (SQLiteConnection connection = new SQLiteConnection(App.dataBasePath))
                {
                    var user = connection.Table<User>().FirstOrDefault(u => u.Username == felhasznaloNevInput);

                    if (user != null)
                    {
                        if (user.Password == jelszoInput)
                        {
                            // Sikeres belépés
                            StationControllerWindow statContrlWin = new StationControllerWindow();
                            statContrlWin.Show();
                            this.Close();
                        }
                        else
                        {
                            // Wrong pass
                            MessageBox.Show("Belépés megtagadva!");
                        }
                    }
                    else
                    {
                        // Wrong user
                        MessageBox.Show("Belépés megtagadva!");
                    }
                }
            }
            else
            {
                // No input(s)
                MessageBox.Show("Jelszó és felhasználónév megadása kötelező!");
            }
        }
    }
}