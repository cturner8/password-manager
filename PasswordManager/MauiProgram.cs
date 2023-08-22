using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PasswordManager.Context;
using PasswordManager.Data;
using PasswordManager.Services;

namespace PasswordManager
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();

            var configBuilder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json");

#if DEBUG
            configBuilder.AddJsonFile("appsettings.Development.json");
#else
            config.AddJsonFile("appsettings.Production.json");
#endif

            IConfiguration appSettings = configBuilder.Build();
            builder.Configuration.AddConfiguration(appSettings);

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            builder.Services.AddSingleton<VaultContext>();

            builder.Services.AddSingleton<WeatherForecastService>();
            builder.Services.AddSingleton<UserService>();


            builder.Services.AddLogging();

            var defaultLogLevel = appSettings.GetValue<LogLevel>("Logging:LogLevel:Default");
            var microsoftLogLevel = appSettings.GetValue<LogLevel>("Logging:LogLevel:Microsoft");
            var systemLogLevel = appSettings.GetValue<LogLevel>("Logging:LogLevel:System");

            builder.Logging
                    .SetMinimumLevel(defaultLogLevel)
                    .AddFilter("Microsoft", microsoftLogLevel)
                    .AddFilter("System", systemLogLevel);

            return builder.Build();
        }
    }
}