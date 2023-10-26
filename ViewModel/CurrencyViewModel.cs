using CoinTrack.Helpers;
using CoinTrack.Model;

namespace CoinTrack.ViewModel;

public class CurrencyViewModel : NotifyPropertyChanged
{
    public static CurrencyDetails Temp { get; set; } = null!;

    public CurrencyViewModel()
    {
        Currency = Temp;
    }

    public CurrencyDetails Currency { get; set; }
}