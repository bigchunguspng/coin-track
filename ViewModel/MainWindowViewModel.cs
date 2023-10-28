using System.Collections.ObjectModel;
using System.Windows.Controls;
using CoinTrack.Helpers;
using CoinTrack.View;

namespace CoinTrack.ViewModel;

public class MainWindowViewModel : NotifyPropertyChanged
{
    public MainWindowViewModel()
    {
        var mainPage = new MainPage();

        TabBar = new TabBar(new ObservableCollection<Page>() { mainPage });

        OpenMainPage = new RelayCommand(_ => TabBar.NewTab(mainPage));
        OpenSearchPage = new RelayCommand(_ => TabBar.NewTab(new SearchPage()));

        Instance = this;
    }

    public static MainWindowViewModel Instance { get; private set; } = null!;


    public TabBar TabBar { get; }


    public RelayCommand OpenMainPage { get; }

    public RelayCommand OpenSearchPage { get; }
}