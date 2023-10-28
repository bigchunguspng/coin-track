using Newtonsoft.Json;

namespace CoinTrack.Model;

/// <summary> Data to be showed on the main page. </summary>
public class Currency : ICoinIdentity
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
    public decimal Price { get; set; }

    [JsonProperty("market_cap")]
    public long MarketCap { get; set; }

    [JsonProperty("market_cap_rank")]
    public int Rank { get; set; }

    [JsonProperty("total_volume")]
    public long Volume24H { get; set; }


    [JsonProperty("price_change_percentage_1h_in_currency")]
    public decimal? PriceChangePercentage1H { get; set; }

    [JsonProperty("price_change_percentage_24h_in_currency")]
    public decimal? PriceChangePercentage24H { get; set; }

    [JsonProperty("price_change_percentage_7d_in_currency")]
    public decimal? PriceChangePercentage7D { get; set; }
}