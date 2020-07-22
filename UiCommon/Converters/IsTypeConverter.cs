using System;
using System.Globalization;
using System.Windows.Data;

namespace UiCommon.Converters
{
    public class IsTypeConverter : IValueConverter
    {
        private static IsTypeConverter _instance;

        public static IsTypeConverter Instance
        {
            get { return _instance ?? (_instance = new IsTypeConverter()); }
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter is Type type)
            {
                if (value == null)
                {
                    return !type.IsValueType;
                }

                if (type.IsValueType)
                {
                    return value.GetType() == type;
                }
                else
                {
                    return type.IsInstanceOfType(value);
                }
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new InvalidOperationException($"{nameof(IsTypeConverter)}不支持TwoWay和OneWayToSource的绑定!");
        }
    }
}