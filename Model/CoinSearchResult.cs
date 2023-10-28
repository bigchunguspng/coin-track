using Newtonsoft.Json;

namespace CoinTrack.Model;

public class CoinSearchResult : ICoinIdentity
{
    [JsonProperty("id")]
    public string Id { get; set; } = null!;

    [JsonProperty("name")]
    public string Name { get; set; } = null!;

    [JsonProperty("symbol")]
    public string Symbol { get; set; } = null!;


    [JsonProperty("market_cap_rank")]
    public int? Rank { get; set; }

    [JsonProperty("thumb")]
    public string Thumb { get; set; } = null!;
}