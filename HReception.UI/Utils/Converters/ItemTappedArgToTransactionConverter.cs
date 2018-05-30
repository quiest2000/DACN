using System;
using System.Globalization;
using Xamarin.Forms;
using HReception.Logic.Services.Interfaces.Payment;

namespace HReception.UI.Utils.Converters
{
    public class ItemTappedArgToTransactionConverter : IValueConverter
    {
        public static ItemTappedArgToTransactionConverter Instance = new ItemTappedArgToTransactionConverter();
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var eventArg = value as ItemTappedEventArgs;
            return eventArg?.Item as TransactionReponse;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}