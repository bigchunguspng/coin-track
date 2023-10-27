using System.Collections.Generic;
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


    [JsonProperty("converted_last")]
    public Dictionary<string, decimal> LastPriceConverted { get; set; } = null!;

    [JsonProperty("converted_volume")]
    public Dictionary<string, decimal> VolumeConverted { get; set; } = null!;


    [JsonProperty("trade_url")]
    public string TradeURL { get; set; } = null!;
}