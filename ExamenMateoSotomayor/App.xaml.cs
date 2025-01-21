using ExamenMateoSotomayor.Repository;
using System.IO;

namespace ExamenMateoSotomayor
{
    public partial class App : Application
    {
        public static DatabaseRepository Database { get; private set; }

        public App()
        {
            InitializeComponent();

            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ExamenMateoSotomayor.db3");
            Database = new DatabaseRepository(dbPath);

            MainPage = new AppShell();
        }
    }
}
