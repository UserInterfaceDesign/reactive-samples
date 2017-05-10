using ReactiveSamples.Twitter.Model;
using System;
using System.Globalization;
using System.Windows.Data;

namespace ReactiveSamples.Twitter.Converters
{
    /// <summary>
    /// Converter for <see cref="Emotion"/>s to images of emojis.
    /// </summary>
    public class EmotionToUriConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Emotion emotion;
            if(Enum.TryParse(value.ToString(), out emotion))
            {
                // cheap way of mapping emojis...
                return new Uri($"pack://application:,,,/ReactiveSamples.Twitter;component/Assets/{emotion}.png");
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
