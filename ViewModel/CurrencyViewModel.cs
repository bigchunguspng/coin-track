using CoinTrack.Helpers;
using CoinTrack.Model;
using CoinTrack.Services;

namespace CoinTrack.ViewModel;

public class CurrencyViewModel : NotifyPropertyChanged
{
    // ViewModel constructor should be parameterless, so Id is passed that way
    public static string TempId { get; set; } = null!;

    public CurrencyViewModel()
    {
        Currency = new CoinGeckoApiClient().GetCurrencyDetails(TempId).Result;
    }

    public CurrencyDetails Currency { get; set; }
}