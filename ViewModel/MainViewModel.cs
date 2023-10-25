using CoinTrack.Helpers;
using CoinTrack.Model;

namespace CoinTrack.ViewModel;

public class MainViewModel : NotifyPropertyChanged
{
    private string _data = null!;

    private TopNCurrencies Currencies { get; } = new();

    public string Data
    {
        get => _data;
        set => SetField(ref _data, value);
    }

    public RelayCommand FetchData { get; }

    public MainViewModel()
    {
        FetchData = new RelayCommand(() => Data = Currencies.FetchData().Result);
    }
}