using System;
using CoinTrack.ViewModel;
using Microsoft.Extensions.DependencyInjection;

namespace CoinTrack.Services;

public static class AppServices
{
    static AppServices()
    {
        var services = new ServiceCollection();

        services.AddSingleton<CoinGeckoApiClient>();
        services.AddSingleton<HyperlinkService>();
        services.AddSingleton<MainPageViewModel>();

        Provider = services.BuildServiceProvider();
    }

    private static IServiceProvider Provider { get; }

    public static MainWindowViewModel MainWindow { get; set; } = null!;

    public static CoinGeckoApiClient CoinsAPI => Get<CoinGeckoApiClient>();

    public static T Get<T>()
    {
        return Provider.GetService<T>()!;
    }
}