using System.Collections.ObjectModel;
using System.Windows.Controls;
using CoinTrack.Helpers;
using CoinTrack.View;

namespace CoinTrack.ViewModel;

public class MainViewModel : NotifyPropertyChanged
{
    private Page _activePage = null!;

    public MainViewModel()
    {
        Pages = new ObservableCollection<Page>() { new TopCurrencies() };
        ActivePage = Pages[0];
    }

    public ObservableCollection<Page> Pages { get; set; }

    public Page ActivePage
    {
        get => _activePage;
        set => SetField(ref _activePage, value);
    }

    //public RelayCommand ClosePage { get; }
}