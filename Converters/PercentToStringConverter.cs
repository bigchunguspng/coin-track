using System;

namespace CoinTrack.Converters;

public class PercentToStringConverter : OneWayConverter<decimal?>
{
    protected override object Convert(decimal? value)
    {
        if (value is null) return "--";

        return $"{Math.Round(value.Value, 1)}%";
    }
}