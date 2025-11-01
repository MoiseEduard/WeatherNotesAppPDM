using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using WeatherNotesApp.Services;

namespace WeatherNotesApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            // 💾 Configurare bază de date
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "app.db");
            builder.Services.AddSingleton<DatabaseService>(s => new DatabaseService(dbPath));

            return builder.Build();
        }
    }
}
