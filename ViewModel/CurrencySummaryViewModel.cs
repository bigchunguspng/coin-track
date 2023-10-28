using CoinTrack.Helpers;
using CoinTrack.Model;
using CoinTrack.View;

namespace CoinTrack.ViewModel;

public class CurrencySummaryViewModel
{
    public CurrencySummaryViewModel(CurrencySummary currency)
    {
        Currency = currency;

        OpenPage = new RelayCommand((_) =>
        {
            CurrencyViewModel.TempId = Currency.Id;
            MainViewModel.Instance.TabBar.NewTab(new CurrencyView());
        });
    }

    public CurrencySummary Currency { get; }

    public RelayCommand OpenPage { get; }

    public string Symbol => Currency.Symbol.ToUpper();
}