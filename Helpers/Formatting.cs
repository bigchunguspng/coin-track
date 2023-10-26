using System;

namespace CoinTrack.Helpers;

public static class Formatting
{
    public static string FormatPercent(decimal value) => $"{Math.Round(value, 1)}%";
    
    public static string FormatPrice(decimal value) => value < 1 ? $"${value:N6}" : $"${value:N}";

    public static string FormatBigMoney(long value) => $"${value:N}";
}