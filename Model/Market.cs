using Newtonsoft.Json;

namespace CoinTrack.Model;

public class Market
{
    [JsonProperty("name")]
    public string Name { get; set; } = null!;

    [JsonProperty("logo")]
    public string Logo { get; set; } = null!;
}