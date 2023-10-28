using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CoinTrack.Helpers;
using CoinTrack.Services;

namespace CoinTrack.ViewModel;

public class SearchViewModel : NotifyPropertyChanged
{
    private DateTime _last = DateTime.Now;
    private ObservableCollection<SearchResultViewModel>? _results;

    public string? SearchText
    {
        get => string.Empty;
        set
        {
            if (string.IsNullOrWhiteSpace(value)) return;

            SetSearchText(value);
        }
    }

    private async void SetSearchText(string value)
    {
        _last = DateTime.Now;

        await Task.Delay(1000).ConfigureAwait(false);

        var time = DateTime.Now;

        if ((time - _last).TotalMilliseconds > 950)
        {
            SearchCoins(value);
        }
    }

    public ObservableCollection<SearchResultViewModel>? SearchResults
    {
        get => _results;
        set => SetField(ref _results, value);
    }

    public void SearchCoins(string query)
    {
        var data = new CoinGeckoApiClient().SearchCoins(query).Result;

        var list = data.Take(20).Where(x => x.Rank is not null).Select(x => new SearchResultViewModel(x));

        SearchResults = new ObservableCollection<SearchResultViewModel>(list);
    }
}