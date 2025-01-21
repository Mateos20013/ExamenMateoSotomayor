using ExamenMateoSotomayor.Repository;

namespace ExamenMateoSotomayor
{
    public partial class App : Application
    {
        public static DatabaseRepository? Database { get; private set; }

        public App(string dbPath)
        {
            InitializeComponent();

            Database = new DatabaseRepository(dbPath);

            MainPage = new AppShell();
        }
    }
}

