using CoinTrack.Model;

namespace CoinTrack.ViewModel;

public class TickerViewModel
{
    public TickerViewModel(Ticker ticker)
    {
        Ticker = ticker;
    }

    public Ticker Ticker { get; set; }

    public string Pair => $"{Ticker.Base}/{Ticker.Target}";
    
    public decimal LastPriceUSD => Ticker.LastPriceConverted["usd"];

    public decimal VolumeUSD => Ticker.VolumeConverted["usd"];
}