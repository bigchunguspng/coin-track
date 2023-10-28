using System.Windows.Controls;
using System.Windows.Navigation;
using CoinTrack.Services;
using CoinTrack.ViewModel;

namespace CoinTrack.View;

public partial class MainPage : Page
{
    public MainPage()
    {
        InitializeComponent();

        var context = new MainPageViewModel();

        DataContext = context;
        context.FetchData.Execute(this);
    }

    private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
    {
        new HyperlinkService().OpenLink(e.Uri.AbsoluteUri);
        e.Handled = true;
    }
}