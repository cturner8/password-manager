using API.Services;
using Database.Context;
using Encryption.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PasswordManager.State;

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

            builder.Services.AddLogging();

            builder.Logging
                    .SetMinimumLevel(appSettings.GetValue<LogLevel>("Logging:LogLevel:Default"))
                    .AddFilter("Microsoft", appSettings.GetValue<LogLevel>("Logging:LogLevel:Microsoft"))
                    .AddFilter("Microsoft.EntityFrameworkCore.Database.Command", appSettings.GetValue<LogLevel>("Logging:LogLevel:Microsoft.EntityFrameworkCore.Database.Command"))
                    .AddFilter("System", appSettings.GetValue<LogLevel>("Logging:LogLevel:System"));

            builder.Services.AddDbContextFactory<VaultContext>();

            var keyDerivationPreferences = new KeyDerivationPreferences()
            {
                Iterations = 600000
            };

            builder.Services.AddSingleton(sp => new KeyDerivationService(
                keyDerivationPreferences.Iterations,
                keyDerivationPreferences.HashAlgorithm,
                keyDerivationPreferences.KeySize
            ));

            builder.Services.AddSingleton<EncryptionService>();
            builder.Services.AddSingleton<UserEncryptionService>();

            builder.Services.AddSingleton<UserService>();

            builder.Services.AddSingleton<VaultService>();
            builder.Services.AddSingleton<VaultLoginService>();
            builder.Services.AddSingleton<VaultNoteService>();

            builder.Services.AddScoped<AuthStateContainer>();

            var app = builder.Build();

            return app;
        }
    }
}