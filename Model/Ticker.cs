using Newtonsoft.Json;

namespace CoinTrack.Model;

public class Ticker
{
    [JsonProperty("base")]
    public string Base { get; set; } = null!;

    [JsonProperty("target")]
    public string Target { get; set; } = null!;

    [JsonProperty("market")]
    public Market Exchange { get; set; } = null!;


    public string Pair => $"{Base}/{Target}";


    [JsonIgnore]
    public decimal LastPrice { get; set; }

    [JsonIgnore]
    public decimal Volume24H { get; set; }


    [JsonProperty("trade_url")]
    public string TradeURL { get; set; } = null!;


    public class Market
    {
        [JsonProperty("name")]
        public string Name { get; set; } = null!;

        [JsonProperty("logo")]
        public string Logo { get; set; } = null!;
    }
}