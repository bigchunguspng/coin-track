using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using CoinTrack.Helpers;
using CoinTrack.Model;
using CoinTrack.Services;
using CoinTrack.View;

namespace CoinTrack.ViewModel;

public class CurrencyViewModel : NotifyPropertyChanged
{
    // ViewModel constructor should be parameterless, so Id is passed that way
    public static string TempId { get; set; } = null!;

    public CurrencyViewModel()
    {
        Currency = new CoinGeckoApiClient().GetCurrencyDetails(TempId).Result;

        var list = new CoinGeckoApiClient().GetCurrencyTickers(Currency.Id).Result;
        var tickers = list.Where(x => x.Target.Length < 10).Select(x => new TickerViewModel(x));

        Tickers = new TickersView() { DataContext = new TickersViewModel() { Tickers = new(tickers) } };

        Indicators = new ObservableCollection<IndicatorValue<string>>()
        {
            new("Market Cap", $"${Currency.MarketCap:N0}"),
            new("Trading Volume", $"${Currency.Volume24H:N0}"),
            new("Volume / Market Cap", $"{Currency.VolumeToMarketCap * 100:N2}%"),
            new("Circulating Supply", FallbackSupply(Currency.CirculatingSupply, "--")),
            new("Total Supply", FallbackSupply(Currency.TotalSupply, "--")),
            new("Max Supply", FallbackSupply(Currency.MaxSupply, "∞"))
        };

        PriceChanges = new ObservableCollection<IndicatorValue<decimal?>>()
        {
            new("1h", Currency.PriceChangePercentage1H),
            new("24h", Currency.PriceChangePercentage24H),
            new("7d", Currency.PriceChangePercentage7D),
            new("14d", Currency.PriceChangePercentage14D),
            new("30d", Currency.PriceChangePercentage30D),
            new("1y", Currency.PriceChangePercentage1Y)
        };
    }

    public CurrencyDetails Currency { get; set; }

    public TickersView Tickers { get; set; }

    public string Symbol => Currency.Symbol.ToUpper();

    public Visibility HighLowVisibility => Currency.Low24H is null ? Visibility.Collapsed : Visibility.Visible;

    public string High24H => GetPriceString(Currency.High24H);

    public string Low24H => GetPriceString(Currency.Low24H);

    // [high_24h] and [high_24h] values returned by API can have no fractional part
    private static string GetPriceString(decimal? price)
    {
        if (price is null) return string.Empty;

        var fraction = price.Value - Math.Truncate(price.Value);
        var isInteger = Math.Abs(fraction) < 0.000_000_001M;
        var isTooCheap = price < 0.000_01M;

        if (isInteger) return $"${price:N0}";
        if (isTooCheap) return $"${price:N8}";
        if (price < 1) return $"${price:N6}";

        return $"${price:N2}";
    }

    private static string FallbackSupply(decimal? value, string fallback)
    {
        return value is null ? fallback : $"{value:N0}";
    }

    public ObservableCollection<IndicatorValue<string>> Indicators { get; }

    public ObservableCollection<IndicatorValue<decimal?>> PriceChanges { get; }

    public record IndicatorValue<T>(string Indicator, T Value);
}