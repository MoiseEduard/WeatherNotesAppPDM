using WeatherNotesApp.Views;
using WeatherNotesApp.Services;

namespace WeatherNotesApp
{
    public partial class App : Application
    {
        public App(DatabaseService databaseService)
        {
            InitializeComponent();

            MainPage = new NavigationPage(new NotesPage(databaseService));
        }
    }
}