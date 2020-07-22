using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace UiCommon.Converters
{
    public class SizeToRectConverter : IValueConverter
    {
        private static SizeToRectConverter _instance;

        public static SizeToRectConverter Instance
        {
            get { return _instance ?? (_instance = new SizeToRectConverter()); }
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is Size size)) return value;
            return new Rect(size);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is Rect rect)) return value;
            return rect.Size;
        }
    }
}