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
                    var felhasznaloRepo = new GenericRepository<User>(App.dataBasePath);
                    List<User> usersList = new List<User>
                    {
                        new User("User1", "User Neve", 3, PassManager.HashPassword("Pass1")),
                        new User("admin", "Admin Teljesnev", 0, PassManager.HashPassword("admin"))
                    };
                    felhasznaloRepo.InsertMultiple(usersList);

                    var itemRepo = new GenericRepository<Item>(App.dataBasePath);
                    List<Item> itemsList = new List<Item> {
                        new Item("Tojás", 95.00, 27.37, "tojas", "Maris néni", "Toás :D (igen, egy darab)"),
                        new Item("Tej", 250.00, 24.32, "tejtermek", "Maris néni", "Tej."),
                        new Item("Kenyér", 400.00, 9.87, "pekaru", "Maris néni", "Kenyér, talán kovászos"),
                        new Item("Sajt", 500.00, 12.00, "tejtermek", "Bözsi néni", "Sajt, nem büdös"),
                        new Item("Alma", 150.00, 40.00, "gyumolcs", "Maris néni", "Piros alma"),
                        new Item("Liszt", 210.00, 10.50, "liszt", "Nagy cég", "Nem hamvak"),
                        new Item("Cukor", 400.00, 0.1, "cukor", "Nagy cég", "Nem só"),
                        new Item("Só", 350.00, 1.50, "so", "Nagy cég", "Nem cukor"),
                        new Item("Fokhagyma", 120.00, 0.00, "zoldseg", "Maris néni", "Vámpírtaszító"),
                        new Item("Káposzta", 150.00, 20.25, "zoldseg", "Maris néni", "Mekk, or something"),
                    };
                    itemRepo.InsertMultiple(itemsList);

                    var customerRepo = new GenericRepository<Customer>(App.dataBasePath);
                    List<Customer> customersList = new List<Customer>
                    {
                        new Customer(1234567891234567, 12, "Vásárló Név", "email@gmail.com", "+361234567"),
                        new Customer(9988776655443322, 0, "Pista", "pista.vasarlo@gmail.com", "+369988776"),
                        new Customer(1000000000000001, 2, "Evelin", "evelin.vasarlo@gmail.com", "+361000001")
                    };
                    customerRepo.InsertMultiple(customersList);
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
                            StationControllerWindow statContrlWin = new StationControllerWindow(user.Id);
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