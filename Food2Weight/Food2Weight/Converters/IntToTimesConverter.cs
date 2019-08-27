using System;
using System.Globalization;
using Xamarin.Forms;

namespace Food2Weight.Converters
{
    public class IntToTimesConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int number = (int) value;
            return number > 0 ? $"{number}x" : string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string times = (string) value;
            return int.Parse(times.Replace("x", string.Empty));
        }
    }
}