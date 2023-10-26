using System.Collections.ObjectModel;
using System.Windows.Controls;
using CoinTrack.Helpers;
using CoinTrack.View;

namespace CoinTrack.ViewModel;

public class MainViewModel : NotifyPropertyChanged
{
    public MainViewModel()
    {
        Instance = this;
        
        TabBar = new TabBar(new ObservableCollection<Page>() { new TopCurrencies() });
    }

    public static MainViewModel Instance { get; private set; } = null!;

    public TabBar TabBar { get; }
}