using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace ReactiveSamples.Twitter.Converters
{
    /// <summary>
    /// Converts a boolean to a visibility state.
    /// </summary>
    public class BooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value != null && value is bool)
            {
                bool b = (bool)value;
                return b ? Visibility.Visible : Visibility.Collapsed;
            }
            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
