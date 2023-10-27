using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Navigation;
using CoinTrack.ViewModel;

namespace CoinTrack.View;

public partial class TopCurrencies : Page
{
    public TopCurrencies()
    {
        InitializeComponent();

        var context = new TopCurrenciesViewModel();

        DataContext = context;
        context.FetchData.Execute(this);
    }

    private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
    {
        Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri) { UseShellExecute = true });
        e.Handled = true;
    }
}