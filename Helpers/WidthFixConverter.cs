using System;
using System.Globalization;
using System.Windows.Data;

namespace CoinTrack.Helpers;

public class WidthFixConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return (double)value - 2D;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}