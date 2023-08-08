using CommunityToolkit.Maui;
using VRCT.Pages;
using VRCT.ViewModels;
using Microsoft.Extensions.Logging;

namespace VRCT;

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
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		builder.Services.AddSingleton<BluetoothLEService>();

		builder.Services.AddSingleton<IConnectivity>(Connectivity.Current);
		builder.Services.AddSingleton<IGeolocation>(Geolocation.Default);
		builder.Services.AddSingleton<IMap>(Map.Default);

		builder.Services.AddSingleton<DevicePageViewModel>();
		builder.Services.AddSingleton<DevicePage>();

		builder.Services.AddSingleton<HeartRatePageViewModel>();
		builder.Services.AddSingleton<HeartRatePage>();

		return builder.Build();
	}
}
