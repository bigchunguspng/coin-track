using System.Collections.ObjectModel;
using System.Windows.Controls;
using CoinTrack.Helpers;
using CoinTrack.View;

namespace CoinTrack.ViewModel;

public class MainViewModel : NotifyPropertyChanged
{
    public MainViewModel()
    {
        var mainPage = new TopCurrencies();

        TabBar = new TabBar(new ObservableCollection<Page>() { mainPage });

        OpenMainPage = new RelayCommand(_ => TabBar.NewTab(mainPage));
        OpenSearchPage = new RelayCommand(_ => TabBar.NewTab(new SearchPage()));

        Instance = this;
    }

    public static MainViewModel Instance { get; private set; } = null!;

    public TabBar TabBar { get; }

    public RelayCommand OpenMainPage { get; }

    public RelayCommand OpenSearchPage { get; }
}