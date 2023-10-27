using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace CoinTrack.Converters;

public abstract class OneWayConverter<T> : IValueConverter
{
    /// <summary> Use this method for simple converters. </summary>
    protected abstract object Convert(T value);

    public virtual object Convert
    (
        object value, Type targetType, object parameter, CultureInfo culture
    )
    {
        return Convert((T)value);
    }

    public object ConvertBack
    (
        object value, Type targetType, object parameter, CultureInfo culture
    )
    {
        return DependencyProperty.UnsetValue;
    }
}