using Common.Extensions;
using System;
using System.Globalization;
using System.Windows.Data;

namespace UiCommon.Converters
{
    public class EnumToDescriptionConverter : IValueConverter
    {
        private static EnumToDescriptionConverter _instance;

        public static EnumToDescriptionConverter Instance
        {
            get { return _instance ?? (_instance = new EnumToDescriptionConverter()); }
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is Enum @enum)) return value;
            return @enum.GetDescription();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}