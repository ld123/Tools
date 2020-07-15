using System;
using System.Collections;
using System.Windows.Markup;

namespace UiCommon.Extensions
{
    [MarkupExtensionReturnType(typeof(IEnumerable))]
    public class GetArrayFromEnumExtension : MarkupExtension
    {
        public Type Type { get; set; }

        public GetArrayFromEnumExtension(Type type)
        {
            Type = type;
            if (!type.IsEnum) throw new InvalidOperationException("只能支持枚举类型!");
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            var array = Enum.GetValues(Type);
            return array;
        }
    }
}