using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using CoinTrack.Helpers;

namespace CoinTrack.ViewModel;

public class TabPage
{
    public TabPage(Page page, TabBar parent)
    {
        Page = page;
        CloseTab = new RelayCommand(delegate { parent.CloseTab(this); });
    }

    public Page Page { get; set; }

    public RelayCommand CloseTab { get; set; }
}

public class TabBar : NotifyPropertyChanged
{
    private TabPage _activeTab = null!;

    public TabBar(ObservableCollection<Page> collection)
    {
        var tabs = collection.Select(page => new TabPage(page, this));

        Tabs = new ObservableCollection<TabPage>(tabs);
        ActiveTab = Tabs[0];

        TabSwitchLeft = new RelayCommand(_ =>
        {
            var count = Tabs.Count;
            ActiveTab = Tabs[(Tabs.IndexOf(ActiveTab) + count - 1) % count];
        });
        TabSwitchRight = new RelayCommand(_ =>
        {
            var count = Tabs.Count;
            ActiveTab = Tabs[(Tabs.IndexOf(ActiveTab) + count + 1) % count];
        });
        CloseActiveTab = new RelayCommand(_ => CloseTab(ActiveTab));
    }

    public ObservableCollection<TabPage> Tabs { get; set; }

    public TabPage ActiveTab
    {
        get => _activeTab;
        set => SetField(ref _activeTab, value);
    }

    public RelayCommand TabSwitchLeft { get; set; }

    public RelayCommand TabSwitchRight { get; set; }

    public RelayCommand CloseActiveTab { get; set; }

    public void NewTab(Page page)
    {
        var equivalent = Tabs.FirstOrDefault(x => x.Page.Title == page.Title);
        if (equivalent is not null)
        {
            ActiveTab = equivalent;
        }
        else
        {
            var tab = new TabPage(page, this);

            Tabs.Add(tab);
            ActiveTab = tab;
        }
    }

    public void CloseTab(TabPage tab)
    {
        var active = Tabs.IndexOf(ActiveTab);
        var closed = Tabs.IndexOf(tab);

        Tabs.Remove(tab);

        if (Tabs.Count == 0)
        {
            Application.Current.MainWindow!.Close();
        }
        else if (active == closed)
        {
            var index = Math.Clamp(active, 0, Tabs.Count - 1);
            ActiveTab = Tabs[index];
        }
    }
}