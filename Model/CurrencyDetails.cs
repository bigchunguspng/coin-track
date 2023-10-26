using Newtonsoft.Json;

namespace CoinTrack.Model;

/// <summary> Data to be showed on a currency page. </summary>
public class CurrencyDetails : CurrencySummary
{
    [JsonProperty("circulating_supply")]
    public decimal CirculatingSupply { get; set; }
    
    [JsonProperty("total_supply")]
    public decimal? TotalSupply { get; set; }
    
    [JsonProperty("max_supply")]
    public decimal? MaxSupply { get; set; }
}