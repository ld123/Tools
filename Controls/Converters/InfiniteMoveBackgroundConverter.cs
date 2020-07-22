using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace Controls.Converters
{
    public class InfiniteMoveBackgroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Border element)
            {
                if (element.Background is VisualBrush brush)
                {
                    return brush.Visual;
                }
                else
                {
                    return element;
                }
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new InvalidOperationException($"{typeof(InfiniteMoveBackgroundConverter)}不支持TwoWay和OneWayToSource的绑定!");
        }
    }
}