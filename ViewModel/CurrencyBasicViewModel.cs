using System;
using CoinTrack.Model;

namespace CoinTrack.ViewModel;

public class CurrencyBasicViewModel
{
    public CurrencyBasicViewModel(CurrencyBasicInfo currency)
    {
        Currency = currency;
    }

    public CurrencyBasicInfo Currency { get; }

    public string Symbol => Currency.Symbol.ToUpper();
    
    public string Price => FormatPrice(Currency.CurrentPrice);
    
    public string PriceChangePercentage1H => FormatPercent(Currency.PriceChangePercentage1H);

    public string PriceChangePercentage24H => FormatPercent(Currency.PriceChangePercentage24H);

    public string PriceChangePercentage7D => FormatPercent(Currency.PriceChangePercentage7D);

    public string Volume24H => FormatBigMoney(Currency.Volume24H);
    
    public string MarketCap => FormatBigMoney(Currency.MarketCap);
    
    
    public static string FormatPercent(decimal value) => $"{Math.Round(value, 1)}%";
    
    public static string FormatPrice(decimal value) => value < 1 ? $"${value:N6}" : $"${value:N}";

    public static string FormatBigMoney(long value) => $"${value:N}";
}