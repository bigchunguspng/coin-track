using System.Windows.Media;

namespace CoinTrack.Converters;

public class PercentToColorConverter : OneWayConverter<decimal>
{
    private readonly SolidColorBrush Red = new(Color.FromRgb(255, 58, 51));
    private readonly SolidColorBrush Green = new(Color.FromRgb(0, 168, 62));
    
    protected override object Convert(decimal value)
    {
        return value < 0 ? Red : Green;
    }
}