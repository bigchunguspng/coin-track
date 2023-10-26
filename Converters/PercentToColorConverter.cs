using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace CoinTrack.Converters;

public class PercentToColorConverter : IValueConverter
{
    private readonly SolidColorBrush Red = new(Color.FromRgb(255, 58, 51));
    private readonly SolidColorBrush Green = new(Color.FromRgb(0, 168, 62));
    
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return (decimal)value < 0 ? Red : Green;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return DependencyProperty.UnsetValue;
    }
}