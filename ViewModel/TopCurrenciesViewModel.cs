using System.Collections.ObjectModel;
using System.Linq;
using CoinTrack.Helpers;
using CoinTrack.Services;

namespace CoinTrack.ViewModel;

public class TopCurrenciesViewModel : NotifyPropertyChanged
{
    private CoinGeckoApiClient ApiClient { get; } = new();

    private ObservableCollection<CurrencySummaryViewModel> _coins = null!;

    public ObservableCollection<CurrencySummaryViewModel> Coins
    {
        get => _coins;
        set => SetField(ref _coins, value);
    }

    public RelayCommand FetchData { get; }

    public TopCurrenciesViewModel()
    {
        FetchData = new RelayCommand((_) =>
        {
            Coins = new(ApiClient.GetTopCurrencies().Result.Select(x => new CurrencySummaryViewModel(x)));
        });
    }
}