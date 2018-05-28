using System;
using System.Globalization;
using HReception.Logic.Services.Interfaces.Patients;
using Xamarin.Forms;

namespace HReception.UI.Utils.Converters
{
    public class ItemTappedArgToPatientConverter:IValueConverter
    {
        public  static ItemTappedArgToPatientConverter Instance=new ItemTappedArgToPatientConverter();
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var eventArg = value as ItemTappedEventArgs;
            return eventArg?.Item as PatientDto;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}