using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using WpfApp3.Commons;
using WpfApp3.UIExtensions;

namespace WpfApp3.Converters
{
    public class PageEnumToControlConverter : IValueConverter
    {
        private static PageEnumToControlConverter _instance;

        public static PageEnumToControlConverter Instance
        {
            get { return _instance ?? (_instance = new PageEnumToControlConverter()); }
        }

        private static readonly Dictionary<MainPage, Type> Map = new Dictionary<MainPage, Type>
        {
            [MainPage.Test] = typeof(ITest),
            [MainPage.Calc] = typeof(ICalc),
        };

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is MainPage page)) return value;
            return GetControl(page);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return MainPage.None;
            if (!(value is FrameworkElement control)) return value;
            return GetPage(control);
        }

        private static FrameworkElement GetControl(MainPage page)
        {
            if (page == MainPage.None) return null;
            var map = Map;
            if (!map.TryGetValue(page, out var type)) throw new NotImplementedException($"页面({page})未创建对应的映射关系!");
            return GetInstanceExtension.GetInstance(type) as FrameworkElement;
        }

        private static MainPage GetPage(FrameworkElement control)
        {
            var map = Map;
            var type = control.GetType();
            foreach (var kvp in map)
            {
                var page = kvp.Key;
                var t = kvp.Value;
                if (t.IsAssignableFrom(type)) return page;
            }

            var msg = $"{control.GetType().Name}-{control.GetHashCode()}";
            throw new NotImplementedException($"控件({msg})未创建对应的映射关系!");
        }
    }
}