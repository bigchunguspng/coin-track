using System.Collections.ObjectModel;
using System.Linq;
using CoinTrack.Helpers;
using CoinTrack.Model;

namespace CoinTrack.ViewModel;

public class TopCurrenciesViewModel : NotifyPropertyChanged
{
    private TopCurrencies Currencies { get; } = new();

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
            Coins = new(Currencies.FetchData().Result.Select(x => new CurrencySummaryViewModel(x)));
        });
    }
}