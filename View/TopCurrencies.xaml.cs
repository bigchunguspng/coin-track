using System.Windows.Controls;
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
}