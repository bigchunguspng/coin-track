using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using CoinTrack.Model;
using Newtonsoft.Json;

namespace CoinTrack.Services;

public class CoinGeckoApiClient
{
    // Cache / Update Frequency: every 45 seconds

    private const string BaseUrl = "https://api.coingecko.com/api/v3/";
    private const string CoinsVsUsd6D = BaseUrl + "coins/markets?vs_currency=usd&precision=6";

    private const string TopCoins = CoinsVsUsd6D + "&price_change_percentage=1h,24h,7d&per_page={0}";
    private const string CoinById = CoinsVsUsd6D + "&price_change_percentage=1h,24h,7d,14d,30d,1y&ids={0}";

    private const string TickersById = BaseUrl + "coins/{0}/tickers?include_exchange_logo=true";

    private readonly HttpClient _client = new()
    {
        DefaultRequestHeaders =
        {
            { "User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:109.0) Gecko/20100101 Firefox/118.0" }
        }
    };

    /// <summary>
    /// Returns top 20 (by default) currencies ranked by market cap.
    /// </summary>
    public async Task<List<CurrencySummary>> GetTopCurrencies(int perPage = 20)
    {
        var url = string.Format(TopCoins, perPage.ToString());

        var data = await GetResponse(url).ConfigureAwait(false);

        //var data = await File.ReadAllTextAsync(@"C:\Users\Osaka\dev\.NET\DCT\CoinTrack\fake_api_coins.json").ConfigureAwait(false);

        return JsonConvert.DeserializeObject<List<CurrencySummary>>(data)!;
    }

    /// <summary>
    /// Returns detail info about cryptocurrency by it's id.
    /// </summary>
    public async Task<CurrencyDetails> GetCurrencyDetails(string id)
    {
        var url = string.Format(CoinById, id);

        var data = await GetResponse(url).ConfigureAwait(false);

        return JsonConvert.DeserializeObject<List<CurrencyDetails>>(data)![0];
    }

    /// <summary>
    /// Returns info about cryptocurrency prices on different markets by it's id.
    /// </summary>
    public async Task<List<Ticker>> GetCurrencyTickers(string id)
    {
        var url = string.Format(TickersById, id);

        var data = await GetResponse(url).ConfigureAwait(false);

        var model = JsonConvert.DeserializeObject<TickersModel>(data)!;

        return model.Tickers;
    }

    /// <summary> Wraps the process making an HTTP GET-request. </summary>
    /// <returns> JSON response string. </returns>
    private async Task<string> GetResponse(string url)
    {
        using var response = await _client.GetAsync(url).ConfigureAwait(false);

        response.EnsureSuccessStatusCode(); // todo handle exceptions

        return await response.Content.ReadAsStringAsync();
    }
}