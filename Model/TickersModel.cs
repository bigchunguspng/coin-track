using System.Collections.Generic;
using Newtonsoft.Json;

namespace CoinTrack.Model;

public class TickersModel
{
    [JsonProperty("name")]
    public string Name { get; set; } = null!;

    [JsonProperty("tickers")]
    public List<Ticker> Tickers { get; set; } = null!;
}