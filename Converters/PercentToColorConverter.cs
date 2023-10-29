using System.Windows.Media;
using CoinTrack.Services;

namespace CoinTrack.Converters;

public class PercentToColorConverter : OneWayConverter<decimal?>
{
    private readonly SolidColorBrush Red = new(Color.FromRgb(255, 58, 51));
    private readonly SolidColorBrush Green = new(Color.FromRgb(0, 168, 62));
    private readonly SolidColorBrush GrayL = new(Color.FromRgb(33, 37, 41));
    private readonly SolidColorBrush GrayD = new(Color.FromRgb(230, 240, 250));

    protected override object Convert(decimal? value)
    {
        if (value is null)
        {
            return AppServices.MainWindow.ActiveTheme.ToString().Contains("Dark") ? GrayD : GrayL;
        }

        return value < 0 ? Red : Green;
    }
}