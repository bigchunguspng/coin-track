using CoinTrack.Helpers;
using CoinTrack.Model;
using CoinTrack.View;

namespace CoinTrack.ViewModel;

public class CoinSearchResultViewModel
{
    public CoinSearchResultViewModel(CoinSearchResult searchResult)
    {
        SearchResult = searchResult;
        OpenPage = new RelayCommand(id =>
        {
            CurrencyViewModel.TempId = (string)id!;
            MainViewModel.Instance.TabBar.NewTab(new CurrencyView());
        });
    }

    public CoinSearchResult SearchResult { get; set; }
    
    public RelayCommand OpenPage { get; }
}