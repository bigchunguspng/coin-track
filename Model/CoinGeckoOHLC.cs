using System;
using System.Collections.Generic;
using ScottPlot;

namespace CoinTrack.Model;

public class CoinGeckoOHLC : IOHLC
{
    private static readonly DateTime Year1970 = new(1970, 1, 1);

    public CoinGeckoOHLC(List<decimal> data)
    {
        DateTime = Year1970 + TimeSpan.FromMilliseconds((long)data[0]);

        Open = (double)data[1];
        High = (double)data[2];
        Low = (double)data[3];
        Close = (double)data[4];

        TimeSpan = TimeSpan.FromMinutes(30);
    }

    public DateTime DateTime { get; set; }

    public TimeSpan TimeSpan { get; set; }

    public double Open { get; set; }

    public double High { get; set; }

    public double Low { get; set; }

    public double Close { get; set; }
}