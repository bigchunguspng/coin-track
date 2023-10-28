using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CoinTrack.Helpers;
using CoinTrack.Services;

namespace CoinTrack.ViewModel;

public class SearchPageViewModel : NotifyPropertyChanged
{
    private string _searchText = string.Empty;
    private DateTime _lastType = DateTime.Now;

    private ObservableCollection<SearchResultViewModel>? _results;


    public string? SearchText
    {
        get => string.IsNullOrWhiteSpace(_searchText) ? string.Empty : _searchText.ToUpper();
        set => SetSearchText(value ?? string.Empty);
    }

    private async void SetSearchText(string value)
    {
        SetField(ref _searchText, value);

        if (string.IsNullOrWhiteSpace(value)) return;

        _lastType = DateTime.Now;

        await Task.Delay(1000).ConfigureAwait(false);

        if ((DateTime.Now - _lastType).TotalMilliseconds > 950)
        {
            SearchCoins(value);
        }
    }

    private void SearchCoins(string query, int amount = 50)
    {
        var data = AppServices.CoinsAPI.SearchCoins(query).Result;

        var list = data.Take(amount).Where(x => x.Rank is not null).Select(x => new SearchResultViewModel(x));

        SearchResults = new ObservableCollection<SearchResultViewModel>(list);
    }

    public ObservableCollection<SearchResultViewModel>? SearchResults
    {
        get => _results;
        set => SetField(ref _results, value);
    }
}