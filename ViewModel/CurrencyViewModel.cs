using System;
using System.Collections.ObjectModel;
using CoinTrack.Helpers;
using CoinTrack.Model;
using CoinTrack.Services;

namespace CoinTrack.ViewModel;

public class CurrencyViewModel : NotifyPropertyChanged
{
    // ViewModel constructor should be parameterless, so Id is passed that way
    public static string TempId { get; set; } = null!;

    public CurrencyViewModel()
    {
        Currency = new CoinGeckoApiClient().GetCurrencyDetails(TempId).Result;

        Indicators = new ObservableCollection<IndicatorValue<string>>()
        {
            new("Market Cap", $"${Currency.MarketCap:N0}"),
            new("Trading Volume", $"${Currency.Volume24H:N0}"),
            new("Volume / Market Cap", $"{Currency.VolumeToMarketCap * 100:N2}%"),
            new("Circulating Supply ", $"{Currency.CirculatingSupply:N0}"),
            new("Total Supply ", $"{Currency.TotalSupply:N0}"),
            new("Max Supply ", Currency.MaxSupply is null ? "∞" : $"{Currency.MaxSupply:N0}")
        };

        PriceChanges = new ObservableCollection<IndicatorValue<decimal>>()
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

    public string Symbol => Currency.Symbol.ToUpper();

    public string High24H => GetPriceString(Currency.High24H);

    public string Low24H => GetPriceString(Currency.Low24H);

    // [high_24h] and [high_24h] values returned by API can have no fractional part
    private string GetPriceString(decimal price)
    {
        var fraction = price - Math.Truncate(price);
        var isInteger = Math.Abs(fraction) < 0.000_000_001M;

        if (isInteger) return $"${price:N0}";
        if (price < 1) return $"${price:N6}";

        return $"${price:N2}";
    }

    public ObservableCollection<IndicatorValue<string>> Indicators { get; }

    public ObservableCollection<IndicatorValue<decimal>> PriceChanges { get; }

    public record IndicatorValue<T>(string Indicator, T Value);
}