using Microsoft.AspNetCore.Components.WebView.Maui;
using MauiBlazorSQLServer.Data;
using Radzen;

namespace MauiBlazorSQLServer;

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
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Services.AddScoped<DialogService>();
        return builder.Build();
    }
}
