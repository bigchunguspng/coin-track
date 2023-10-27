using System.Collections.ObjectModel;

namespace CoinTrack.ViewModel;

public class TickersViewModel
{
    public ObservableCollection<TickerViewModel> Tickers { get; set; } = null!;
}