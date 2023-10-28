using System.Collections.ObjectModel;
using System.Linq;
using CoinTrack.Helpers;
using CoinTrack.Services;

namespace CoinTrack.ViewModel;

public class MainPageViewModel : NotifyPropertyChanged
{
    private CoinGeckoApiClient ApiClient { get; } = new();

    private ObservableCollection<CurrencyViewModel> _coins = null!;

    public ObservableCollection<CurrencyViewModel> Coins
    {
        get => _coins;
        set => SetField(ref _coins, value);
    }

    public RelayCommand FetchData { get; }

    public MainPageViewModel()
    {
        FetchData = new RelayCommand((_) =>
        {
            Coins = new(ApiClient.GetTopCurrencies().Result.Select(x => new CurrencyViewModel(x)));
        });
    }
}