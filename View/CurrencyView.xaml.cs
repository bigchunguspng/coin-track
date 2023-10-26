using System.Windows.Controls;
using CoinTrack.ViewModel;

namespace CoinTrack.View;

public partial class CurrencyView : Page
{
    public CurrencyView()
    {
        InitializeComponent();

        // This enables switching to a tab with the currency if it's already opened.
        Title = ((CurrencyViewModel)DataContext).Currency.Name;
    }
}