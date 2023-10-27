using System;

namespace CoinTrack.Converters;

public class PercentToStringConverter : OneWayConverter<decimal>
{
    protected override object Convert(decimal value)
    {
        return $"{Math.Round(value, 1)}%";
    }
}