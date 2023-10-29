using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CoinTrack.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CoinTrack.Services;

public class CoinGeckoApiClient
{
    private const string BaseUrl = "https://api.coingecko.com/api/v3/";
    private const string VsUSD = "?vs_currency=usd&precision=8";
    private const string CoinsVsUSD = BaseUrl + "coins/markets" + VsUSD;

    private const string TopCoins = CoinsVsUSD + "&price_change_percentage=1h,24h,7d&per_page={0}";
    private const string CoinById = CoinsVsUSD + "&price_change_percentage=1h,24h,7d,14d,30d,1y&ids={0}";

    private const string CoinOHLC = BaseUrl + "coins/{0}/ohlc" + VsUSD + "&days=1";

    private const string TickersById = BaseUrl + "coins/{0}/tickers?include_exchange_logo=true&order=volume_desc";

    private const string Search = BaseUrl + "search?query={0}";

    private const string TOP = "top", COIN = "coin", OHLC = "ohlc", TICKERS = "tickers", SEARCH = "search";

    private readonly HttpClient _client = new()
    {
        DefaultRequestHeaders =
        {
            { "User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:109.0) Gecko/20100101 Firefox/118.0" }
        }
    };

    public CoinGeckoApiClient()
    {
        Cache.Register(TOP, TimeSpan.FromSeconds(45));
    }

    private CacheService Cache { get; } = new();

    /// <summary>
    /// Returns top 20 (by default) currencies ranked by market cap.
    /// </summary>
    public async Task<List<Currency>> GetTopCurrencies(int perPage = 20)
    {
        if (Cache.IsRelevant<List<Currency>>(TOP, out var data))
        {
            return data;
        }

        var url = string.Format(TopCoins, perPage.ToString());

        var json = await GetResponse(url).ConfigureAwait(false);

        var result = JsonConvert.DeserializeObject<List<Currency>>(json)!;

        Cache.Update(TOP, result);

        return result;
    }

    /// <summary>
    /// Returns detail info about cryptocurrency by it's id.
    /// </summary>
    public async Task<CurrencyDetails> GetCurrencyDetails(string id)
    {
        var key = $"{COIN}.{id}";
        if (Cache.IsRelevant<CurrencyDetails>(key, out var data))
        {
            return data;
        }

        var url = string.Format(CoinById, id);

        var json = await GetResponse(url).ConfigureAwait(false);

        var result = JsonConvert.DeserializeObject<List<CurrencyDetails>>(json)![0];

        Cache.Register(key, TimeSpan.FromSeconds(45));
        Cache.Update(key, result);

        return result;
    }

    /// <summary>
    /// Returns cryptocurrency OHLC data for the last 24 hours by it's id.
    /// </summary>
    public async Task<List<CoinGeckoOHLC>> GetCurrencyOHLC(string id)
    {
        var key = $"{OHLC}.{id}";
        if (Cache.IsRelevant<List<CoinGeckoOHLC>>(key, out var data))
        {
            return data;
        }

        var url = string.Format(CoinOHLC, id);

        var json = await GetResponse(url).ConfigureAwait(false);

        var result = JsonConvert
            .DeserializeObject<List<List<decimal>>>(json)!
            .Select(x => new CoinGeckoOHLC(x)).ToList();

        Cache.Register(key, TimeSpan.FromMinutes(5));
        Cache.Update(key, result);

        return result;
    }

    /// <summary>
    /// Returns info about cryptocurrency prices on different markets by it's id.
    /// </summary>
    public async Task<List<Ticker>> GetCurrencyTickers(string id)
    {
        var key = $"{TICKERS}.{id}";
        if (Cache.IsRelevant<List<Ticker>>(key, out var data))
        {
            return data;
        }

        var url = string.Format(TickersById, id);

        var json = await GetResponse(url).ConfigureAwait(false);

        var tickers = SelectJToken(json, "tickers").Select(delegate(JToken jt)
        {
            var ticker = jt.ToObject<Ticker>()!;

            ticker.LastPrice = (decimal)jt.SelectToken("converted_last.usd")!;
            ticker.Volume24H = (decimal)jt.SelectToken("converted_volume.usd")!;

            return ticker;
        }).ToList();

        Cache.Register(key, TimeSpan.FromMinutes(2));
        Cache.Update(key, tickers);

        return tickers;
    }

    /// <summary>
    /// Searches for cryptocurrencies by it's id, name, or symbol.
    /// </summary>
    public async Task<List<CoinSearchResult>> SearchCoins(string query)
    {
        var key = $"{SEARCH}.{query}";
        if (Cache.IsRelevant<List<CoinSearchResult>>(key, out var data))
        {
            return data;
        }

        var url = string.Format(Search, query);

        var json = await GetResponse(url).ConfigureAwait(false);

        var coins = SelectJToken(json, "coins").Select(jt => jt.ToObject<CoinSearchResult>()!).ToList();

        Cache.Register(key, TimeSpan.FromMinutes(15));
        Cache.Update(key, coins);

        return coins;
    }

    /// <summary> Wraps the process making an HTTP GET-request. </summary>
    /// <returns> JSON response string. </returns>
    private async Task<string> GetResponse(string url)
    {
        using var response = await _client.GetAsync(url).ConfigureAwait(false);

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadAsStringAsync();
    }

    private JToken SelectJToken(string json, string path)
    {
        return JObject.Parse(json).SelectToken(path)!;
    }
}