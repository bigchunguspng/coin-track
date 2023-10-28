using System.Collections.ObjectModel;
using System.Linq;
using CoinTrack.Helpers;
using CoinTrack.Services;

namespace CoinTrack.ViewModel;

public class MainPageViewModel : NotifyPropertyChanged
{
    private ObservableCollection<CurrencyViewModel> _coins = null!;

    public MainPageViewModel()
    {
        FetchData = new RelayCommand(_ =>
        {
            var coins = AppServices.Get<CoinGeckoApiClient>().GetTopCurrencies().Result;
            
            Coins = new ObservableCollection<CurrencyViewModel>(coins.Select(x => new CurrencyViewModel(x)));
        });
    }

    public ObservableCollection<CurrencyViewModel> Coins
    {
        get => _coins;
        set => SetField(ref _coins, value);
    }

    public RelayCommand FetchData { get; }
}