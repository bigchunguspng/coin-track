using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using CoinTrack.Helpers;

namespace CoinTrack.View;

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
    }

    public ObservableCollection<TabPage> Tabs { get; set; }

    public TabPage ActiveTab
    {
        get => _activeTab;
        set => SetField(ref _activeTab, value);
    }

    public void NewTab(Page page)
    {
        var equivalent = Tabs.FirstOrDefault(x => x.Page.Title == page.Title); // not working
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

        if (active != closed) return;

        if (Tabs.Count == 0)
        {
            Application.Current.MainWindow!.Close();
        }
        else if (active > 0)
        {
            ActiveTab = Tabs[--active];
        }
        else
        {
            ActiveTab = Tabs[++active];
        }
    }
}