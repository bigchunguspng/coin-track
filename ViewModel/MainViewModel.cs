using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using CoinTrack.Helpers;
using CoinTrack.View;

namespace CoinTrack.ViewModel;

public class MainViewModel : NotifyPropertyChanged
{
    public static MainViewModel Instance { get; private set; } = null!;

    private Page _activePage = null!;

    public MainViewModel()
    {
        Instance = this;
        
        Pages = new ObservableCollection<Page>() { new TopCurrencies() };
        ActivePage = Pages[0];

        NewPage = new RelayCommand(parameter =>
        {
            ArgumentNullException.ThrowIfNull(parameter);

            var page = (Page)parameter;
            Pages.Add(page);
            ActivePage = page;
        });
    }

    public ObservableCollection<Page> Pages { get; set; }

    public Page ActivePage
    {
        get => _activePage;
        set => SetField(ref _activePage, value);
    }

    public RelayCommand NewPage { get; }
    //public RelayCommand ClosePage { get; }
}