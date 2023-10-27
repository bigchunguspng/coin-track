namespace CoinTrack.Converters;

public class ActualWidthFixConverter : OneWayConverter<double>
{
    protected override object Convert(double value)
    {
        return value - 2D;
    }
}