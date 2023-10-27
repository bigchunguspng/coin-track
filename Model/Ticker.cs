using Newtonsoft.Json;

namespace CoinTrack.Model;

public class Ticker
{
    [JsonProperty("base")]
    public string Base { get; set; } = null!;

    [JsonProperty("target")]
    public string Target { get; set; } = null!;

    [JsonProperty("market")]
    public Market Market { get; set; } = null!;


    [JsonProperty("last")]
    public decimal LastPrice { get; set; }

    [JsonProperty("volume")]
    public decimal Volume { get; set; }


    [JsonProperty("trade_url")]
    public string TradeURL { get; set; } = null!;
}