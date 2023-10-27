namespace CoinTrack.Converters;

public class PriceToStringConverter : OneWayConverter<decimal>
{
    protected override object Convert(decimal value)
    {
        return value < 1 ? $"${value:N6}" : $"${value:N2}";
    }
}