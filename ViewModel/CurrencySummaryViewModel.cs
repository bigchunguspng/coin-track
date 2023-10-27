using CoinTrack.Helpers;
using CoinTrack.Model;
using CoinTrack.View;

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

    public RelayCommand OpenAsNewPage { get; }

    public string Symbol => Currency.Symbol.ToUpper();
}