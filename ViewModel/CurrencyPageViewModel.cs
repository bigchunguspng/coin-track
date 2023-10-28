using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using CoinTrack.Helpers;
using CoinTrack.Model;
using CoinTrack.Services;
using CoinTrack.View;

namespace CoinTrack.ViewModel;

public class CurrencyPageViewModel : NotifyPropertyChanged
{
    /// <summary>
    /// Use this factory method for creating an object of this class. 
    /// </summary>
    public static CurrencyPageViewModel New(string id)
    {
        var details = AppServices.CoinsAPI.GetCurrencyDetails(id);
        var markets = AppServices.CoinsAPI.GetCurrencyTickers(id);

        Task.WhenAll(details, markets).Wait();

        return new CurrencyPageViewModel()
        {
            Currency = details.Result,
            Tickers = new TickersView()
            {
                DataContext = new TickersViewModel() { Tickers = new(markets.Result) }
            }
        };
    }

    public CurrencyDetails Currency { get; private set; } = null!;

    public TickersView Tickers { get; private set; } = null!;


    public Visibility PriceRangeVisibility => Currency.Low24H is null ? Visibility.Collapsed : Visibility.Visible;

    public string High24H => GetPriceString(Currency.High24H);

    public string Low24H => GetPriceString(Currency.Low24H);


    /// <summary>
    /// Use this specifically for formatting Low and High prices for the last 24 hours.
    /// </summary>
    private static string GetPriceString(decimal? price)
    {
        if (price is null) return string.Empty;

        var isTooCheap = price < 0.000_01M;

        if (IsInteger()) return $"${price:N0}";
        if (isTooCheap) return $"${price:N8}";
        if (price < 1) return $"${price:N6}";

        return $"${price:N2}";

        bool IsInteger() => Math.Abs(price.Value - Math.Truncate(price.Value)) < 0.000_000_001M;
    }


    public ObservableCollection<Indicator<string>> Indicators => new()
    {
        new(
            "Market Cap",
            $"${Currency.MarketCap:N0}"),
        new(
            "Trading Volume",
            $"${Currency.Volume24H:N0}"),
        new(
            "Volume / Market Cap",
            $"{Currency.VolumeToMarketCap * 100:N2}%"),
        new(
            "Circulating Supply",
            FallbackSupply(Currency.CirculatingSupply, "--")),
        new(
            "Total Supply",
            FallbackSupply(Currency.TotalSupply, "--")),
        new(
            "Max Supply",
            FallbackSupply(Currency.MaxSupply, "∞"))
    };

    private static string FallbackSupply(decimal? value, string fallback)
    {
        return value is null ? fallback : $"{value:N0}";
    }

    public ObservableCollection<Indicator<decimal?>> PriceDynamics => new()
    {
        new("1h", Currency.PriceChangePercentage1H),
        new("24h", Currency.PriceChangePercentage24H),
        new("7d", Currency.PriceChangePercentage7D),
        new("14d", Currency.PriceChangePercentage14D),
        new("30d", Currency.PriceChangePercentage30D),
        new("1y", Currency.PriceChangePercentage1Y)
    };

    public record Indicator<T>(string Label, T Value);
}