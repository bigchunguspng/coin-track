using System.Windows.Controls;
using CoinTrack.Model;
using CoinTrack.ViewModel;

namespace CoinTrack.View;

public partial class CurrencyPage : Page
{
    public CurrencyPage()
    {
        InitializeComponent();
    }

    public CurrencyPage(ICoinIdentity coin)
    {
        InitializeComponent();

        Title = coin.Name;
        DataContext = CurrencyPageViewModel.New(coin.Id);
    }
}