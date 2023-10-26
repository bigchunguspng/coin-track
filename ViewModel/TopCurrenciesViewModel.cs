using System.Collections.ObjectModel;
using System.Linq;
using CoinTrack.Helpers;
using CoinTrack.Model;

namespace CoinTrack.ViewModel;

public class TopCurrenciesViewModel : NotifyPropertyChanged
{
    private TopNCurrencies Currencies { get; } = new();

    private ObservableCollection<CurrencyBasicViewModel> _coins = null!;

    public ObservableCollection<CurrencyBasicViewModel> Coins
    {
        get => _coins;
        set => SetField(ref _coins, value);
    }

    public RelayCommand FetchData { get; }

    public TopCurrenciesViewModel()
    {
        FetchData = new RelayCommand(() =>
        {
            Coins = new(Currencies.FetchData().Result.Select(x => new CurrencyBasicViewModel(x)));
        });
    }
}