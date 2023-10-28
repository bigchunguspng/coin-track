using Newtonsoft.Json;

namespace CoinTrack.Model;

/// <summary> Data to be showed on a currency page. </summary>
public class CurrencyDetails : CurrencySummary
{
    [JsonProperty("circulating_supply")]
    public decimal? CirculatingSupply { get; set; }

    [JsonProperty("total_supply")]
    public decimal? TotalSupply { get; set; }

    [JsonProperty("max_supply")]
    public decimal? MaxSupply { get; set; }


    public decimal VolumeToMarketCap => Volume24H / (decimal)MarketCap;


    [JsonProperty("price_change_percentage_14d_in_currency")]
    public decimal? PriceChangePercentage14D { get; set; }

    [JsonProperty("price_change_percentage_30d_in_currency")]
    public decimal? PriceChangePercentage30D { get; set; }

    [JsonProperty("price_change_percentage_1y_in_currency")]
    public decimal? PriceChangePercentage1Y { get; set; }


    [JsonProperty("high_24h")]
    public decimal? High24H { get; set; }

    [JsonProperty("low_24h")]
    public decimal? Low24H { get; set; }


    // todo ath/l (price, %, date)
}