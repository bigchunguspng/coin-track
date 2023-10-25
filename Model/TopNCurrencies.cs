using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CoinTrack.Model;

public class TopNCurrencies
{
    // Cache / Update Frequency: every 45 seconds

    private const string _url = "https://api.coingecko.com/api/v3/coins/markets?vs_currency=usd&per_page={0}&price_change_percentage=1h,24h,7d&precision=6";

    private readonly HttpClient _client = new()
    {
        DefaultRequestHeaders =
        {
            { "User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:109.0) Gecko/20100101 Firefox/118.0" }
        }
    };

    private List<CurrencyBasicInfo> Coins { get; set; } = null!;


    public async Task<List<CurrencyBasicInfo>> FetchData(int perPage = 20)
    {
        var url = string.Format(_url, perPage.ToString());

        using var response = await _client.GetAsync(url).ConfigureAwait(false);
        
        response.EnsureSuccessStatusCode(); // todo handle exceptions
        
        var data = await response.Content.ReadAsStringAsync(); // string

        Coins = JsonConvert.DeserializeObject<List<CurrencyBasicInfo>>(data)!;

        return Coins;
    }
}