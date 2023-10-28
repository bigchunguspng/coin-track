using System.Collections.ObjectModel;
using System.Windows.Controls;
using CoinTrack.Helpers;
using CoinTrack.Services;
using CoinTrack.View;

namespace CoinTrack.ViewModel;

public class MainWindowViewModel : NotifyPropertyChanged
{
    public MainWindowViewModel()
    {
        AppServices.MainWindow = this;

        var mainPage = new MainPage();

        TabBar = new TabBar(new ObservableCollection<Page>() { mainPage });

        OpenMainPage = new RelayCommand(_ => TabBar.NewTab(mainPage));
        OpenSearchPage = new RelayCommand(_ => TabBar.NewTab(new SearchPage()));
    }

    public TabBar TabBar { get; }


    public RelayCommand OpenMainPage { get; }

    public RelayCommand OpenSearchPage { get; }
}