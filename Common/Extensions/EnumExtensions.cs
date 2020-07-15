using Common.Common;
using System;
using System.ComponentModel;

namespace Common.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDescription(this Enum enumeration)
        {
            var type = enumeration.GetType();
            var memInfo = type.GetMember(enumeration.ToString());
            var attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attrs.Length == 0)
            {
                Logger.Fatal(new InvalidOperationException($"该枚举({type.FullName})不存在Description特性!"));
            }

            return ((DescriptionAttribute) attrs[0]).Description;
        }
    }
}