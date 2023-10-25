using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CoinTrack.Model;

public class TopNCurrencies
{
    // Cache / Update Frequency: every 45 seconds

    private const string _url = "https://api.coingecko.com/api/v3/coins/markets?vs_currency=usd&per_page={0}&price_change_percentage=1h,24h,7d&precision=2";

    private readonly HttpClient _client = new()
    {
        DefaultRequestHeaders =
        {
            { "User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:109.0) Gecko/20100101 Firefox/118.0" }
        }
    };

    public List<CryptoCurrencyBasicInfo> Coins { get; set; } = null!;

    public async Task<string> FetchData(int perPage = 20)
    {
        var url = string.Format(_url, perPage.ToString());

        using var response = await _client.GetAsync(url).ConfigureAwait(false);
        
        response.EnsureSuccessStatusCode();
        
        var data = await response.Content.ReadAsStringAsync();

        return data;
    }
}

public class CryptoCurrencyBasicInfo
{
    [JsonProperty("id")]
    public string Id { get; set; } = null!;

    [JsonProperty("symbol")]
    public string Symbol { get; set; } = null!;

    [JsonProperty("name")]
    public string Name { get; set; } = null!;

    [JsonProperty("image")]
    public string Image { get; set; } = null!;


    /// <summary> Price in USD. </summary>
    [JsonProperty("current_price")]
    public decimal CurrentPrice { get; set; }

    [JsonProperty("market_cap")]
    public long MarketCap { get; set; }

    [JsonProperty("market_cap_rank")]
    public int Rank { get; set; }

    [JsonProperty("total_volume")]
    public long Volume24H { get; set; }


    [JsonProperty("price_change_percentage_1h_in_currency")]
    public decimal PriceChangePercentage1H { get; set; }

    [JsonProperty("price_change_percentage_24h_in_currency")]
    public decimal PriceChangePercentage24H { get; set; }

    [JsonProperty("price_change_percentage_7d_in_currency")]
    public decimal PriceChangePercentage7D { get; set; }
}