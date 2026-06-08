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
                    User user = new User("User1", "User Neve", 0, PassManager.HashPassword("Pass1"));
                    var felhasznaloRepo = new GenericRepository<User>(App.dataBasePath);
                    felhasznaloRepo.Insert(user);
                }
                catch (Exception ar)
                {
                    
                    throw;
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