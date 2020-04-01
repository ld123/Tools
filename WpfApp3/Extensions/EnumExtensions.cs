using System;
using System.ComponentModel;

namespace WpfApp3.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDescription(this Enum enumeration)
        {
            var type = enumeration.GetType();
            var memInfo = type.GetMember(enumeration.ToString());
            var attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
            return ((DescriptionAttribute) attrs[0]).Description;
        }
    }
}