using Turismo.MVVM.Views;
using Turismo.Services;

namespace Turismo
{
    public partial class App : Application
    {
        static DatabaseService? database;

        public static DatabaseService Database
        {
            get
            {
                if (database == null)
                {
                    string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Turismo.db3");
                    database = new DatabaseService(dbPath);
                }
                return database;
            }
        }
        public App()
        {
            InitializeComponent();

            MainPage = new AppContainer();
        }
    }
}
