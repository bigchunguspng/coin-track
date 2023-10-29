using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
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

        SwitchThemes();

        OpenMainPage = new RelayCommand(_ => TabBar.NewTab(mainPage));
        OpenSearchPage = new RelayCommand(_ => TabBar.NewTab(new SearchPage()));
        SwitchTheme = new RelayCommand(_ => SwitchThemes());
    }

    public TabBar TabBar { get; }


    public RelayCommand OpenMainPage { get; }

    public RelayCommand OpenSearchPage { get; }

    public RelayCommand SwitchTheme { get; }


    private List<Uri> Themes { get; } = new()
    {
        new Uri("Themes/Light.xaml", UriKind.Relative),
        new Uri("Themes/Dark.xaml", UriKind.Relative)
    };

    private Uri ActiveTheme { get; set; } = null!;

    private void SwitchThemes()
    {
        ActiveTheme = Themes[(Themes.IndexOf(ActiveTheme) + 1) % Themes.Count];

        AppServices.CurrentTheme = new ResourceDictionary() { Source = ActiveTheme };

        ThemeChanged?.Invoke(this, EventArgs.Empty);
    }

    public event EventHandler? ThemeChanged;
}