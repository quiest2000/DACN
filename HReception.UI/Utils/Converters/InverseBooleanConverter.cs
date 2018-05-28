using System;
using System.Globalization;
using Xamarin.Forms;

namespace HReception.UI.Utils.Converters
{
    public class InverseBooleanConverter : IValueConverter
    {
        public static InverseBooleanConverter Instance = new InverseBooleanConverter();
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var b = value as bool?;
            return !b.HasValue || !b.Value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
