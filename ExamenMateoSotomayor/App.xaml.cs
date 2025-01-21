using ExamenMateoSotomayor.Repository;
using System.IO;

namespace ExamenMateoSotomayor
{
    public partial class App : Application
    {
        private static DatabaseRepository _database;

        public static DatabaseRepository Database
        {
            get
            {
                if (_database == null)
                {
                    string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "buscadorpeliculas.db3");
                    _database = new DatabaseRepository(dbPath);
                }
                return _database;
            }
        }

        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();
        }
    }
}
