using System.Configuration;
using System.Data;
using System.Windows;

namespace cashierStation_zaroProject
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        static string dataBase = "cashierDataDB.db";
        static string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        public static string dataBasePath = System.IO.Path.Combine(path, dataBase);
    }
}
