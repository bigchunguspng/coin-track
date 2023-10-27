using CoinTrack.Helpers;
using CoinTrack.Model;
using CoinTrack.View;
using Formatting = CoinTrack.Helpers.Formatting;

namespace CoinTrack.ViewModel;

public class CurrencySummaryViewModel
{
    public CurrencySummaryViewModel(CurrencySummary currency)
    {
        Currency = currency;

        OpenAsNewPage = new RelayCommand((_) =>
        {
            CurrencyViewModel.TempId = Currency.Id;
            MainViewModel.Instance.TabBar.NewTab(new CurrencyView());
        });
    }

    public CurrencySummary Currency { get; }

    public string Symbol => Currency.Symbol.ToUpper();
    
    public string Price => Formatting.FormatPrice(Currency.CurrentPrice);
    
    public string PriceChangePercentage1H => Formatting.FormatPercent(Currency.PriceChangePercentage1H);

    public string PriceChangePercentage24H => Formatting.FormatPercent(Currency.PriceChangePercentage24H);

    public string PriceChangePercentage7D => Formatting.FormatPercent(Currency.PriceChangePercentage7D);

    public string Volume24H => Formatting.FormatBigMoney(Currency.Volume24H);
    
    public string MarketCap => Formatting.FormatBigMoney(Currency.MarketCap);


    public RelayCommand OpenAsNewPage { get; }
}