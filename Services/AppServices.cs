using System;
using Microsoft.Extensions.DependencyInjection;

namespace CoinTrack.Services;

public static class AppServices
{
    static AppServices()
    {
        var services = new ServiceCollection();

        services.AddSingleton<CoinGeckoApiClient>();
        services.AddSingleton<HyperlinkService>();

        Provider = services.BuildServiceProvider();
    }

    private static IServiceProvider Provider { get; }

    public static T Get<T>()
    {
        return Provider.GetService<T>()!;
    }
}