using System;

namespace CoinTrack.Converters;

public class PercentToArrowConverter : OneWayConverter<decimal>
{
    protected override object Convert(decimal value)
    {
        return $"{(value > 0 ? '↗' : '↘')} {Math.Abs(Math.Round(value, 1))}%";
    }
}