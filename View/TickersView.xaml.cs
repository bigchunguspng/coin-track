using System.Windows.Controls;
using System.Windows.Navigation;
using CoinTrack.Services;

namespace CoinTrack.View;

public partial class TickersView : UserControl
{
    public TickersView()
    {
        InitializeComponent();
    }

    private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
    {
        AppServices.Get<HyperlinkService>().OpenLink(e.Uri.AbsoluteUri);
        e.Handled = true;
    }
}