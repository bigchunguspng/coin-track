using System;

namespace CoinTrack.Converters;

public class PercentToArrowConverter : OneWayConverter<decimal?>
{
    protected override object Convert(decimal? value)
    {
        if (value is null) return string.Empty;

        return $"{(value > 0 ? '↗' : '↘')} {Math.Abs(Math.Round(value.Value, 1))}%";
    }
}