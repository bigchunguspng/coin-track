using CoinTrack.Helpers;
using CoinTrack.Model;
using CoinTrack.Services;
using CoinTrack.View;

namespace CoinTrack.ViewModel;

public class SearchResultViewModel
{
    public SearchResultViewModel(CoinSearchResult searchResult)
    {
        SearchResult = searchResult;

        OpenPage = new RelayCommand(_ =>
        {
            AppServices.MainWindow.TabBar.NewTab(new CurrencyPage(searchResult));
        });
    }

    public CoinSearchResult SearchResult { get; }

    public RelayCommand OpenPage { get; }
}