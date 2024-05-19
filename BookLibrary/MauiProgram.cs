using AiForms.Settings;
using BookLibrary.Data;
using BookLibrary.Resources.Fonts;
using BookLibrary.ViewModels;
using BookLibrary.Views;
using Microsoft.Extensions.Logging;

namespace BookLibrary;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UsePrism(prism =>
                prism.ConfigureModuleCatalog(moduleCatalog => { ConfigureModuleCatalog(moduleCatalog); })
                    .RegisterTypes(containerRegistry => { RegisterTypes(containerRegistry); })
                    .CreateWindow("HomePage")
            )
            .RegisterFonts()
            .ConfigureMauiHandlers(handler =>
            {
                handler.AddSettingsViewHandler();
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }

    static Database database;

    public static Database Database
    {
        get
        {
            if (database == null)
            {
                database = new Database(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "task.db3"));
            }
            return database;
        }
    }
    
    private static void RegisterTypes(IContainerRegistry containerRegistry)
    {
        containerRegistry.RegisterForNavigation<HomePage, HomePageViewModel>("HomePage")
            .RegisterInstance(SemanticScreenReader.Default);
        containerRegistry.RegisterForNavigation<AddBookPage, AddBookPageViewModel>("AddBookPage")
            .RegisterInstance(SemanticScreenReader.Default);
    }

    private static void ConfigureModuleCatalog(IModuleCatalog moduleCatalog) { }
}