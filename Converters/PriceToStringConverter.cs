namespace CoinTrack.Converters;

public class PriceToStringConverter : OneWayConverter<decimal>
{
    protected override object Convert(decimal value)
    {
        return value < 1 ? value < 0.000_01M ? $"${value:N8}" : $"${value:N6}" : $"${value:N2}";
    }
}