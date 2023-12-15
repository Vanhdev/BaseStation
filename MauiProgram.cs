using Microsoft.Extensions.Logging;
using BaseStation.Data;
using dymaptic.GeoBlazor.Core;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using Microsoft.Extensions.FileProviders;

namespace BaseStation;

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
        var assembly = typeof(App).GetTypeInfo().Assembly;
        var config = new ConfigurationBuilder()
			.AddJsonFile(new EmbeddedFileProvider(assembly),"appsettings.json", optional: false, false)
			.Build();

        builder.Configuration.AddConfiguration(config);

        builder.Services.AddMauiBlazorWebView();

#if ANDROID
        builder.Services.AddTransient<ICellInformationService, CellInformationService>();
		builder.Services.AddSingleton<MainActivity>();
#endif

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
		builder.Logging.AddDebug();
#endif

        builder.Services.AddGeoBlazor();
        builder.Services.AddSingleton<WeatherForecastService>();
        return builder.Build();
	}
}
