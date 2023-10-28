using CoinTrack.Helpers;
using CoinTrack.Model;
using CoinTrack.View;

namespace CoinTrack.ViewModel;

public class CurrencyViewModel
{
    public CurrencyViewModel(Currency currency)
    {
        Currency = currency;

        OpenPage = new RelayCommand(_ =>
        {
            MainWindowViewModel.Instance.TabBar.NewTab(new CurrencyPage(Currency));
        });
    }

    public Currency Currency { get; }

    public RelayCommand OpenPage { get; }
}