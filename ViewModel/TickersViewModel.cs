using System.Collections.ObjectModel;
using CoinTrack.Model;

namespace CoinTrack.ViewModel;

public class TickersViewModel
{
    public ObservableCollection<Ticker> Tickers { get; set; } = null!;
}