using System;
using System.Windows;
using System.Windows.Markup;
using TinyIoC;

namespace WpfApp3.UIExtensions
{
    public class GetInstanceExtension : MarkupExtension
    {
        public Type Type { get; }

        public GetInstanceExtension(Type type)
        {
            Type = type;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            // 如果没有服务，则直接返回。
            if (!(serviceProvider.GetService(typeof(IProvideValueTarget)) is IProvideValueTarget service)) return null;
            // MarkupExtension 在样式模板中，返回 this 以延迟提供值。
            if (service.TargetObject.GetType().Name.EndsWith("SharedDp")) return this;
            if (!(service.TargetObject is FrameworkElement)) return this;

            return GetInstance(Type);
        }

        public static TOut GetInstance<TIn, TOut>() where TOut : class
        // where TOut : TIn
        {
            return GetInstance(typeof(TIn)) as TOut;
        }

        public static T GetInstance<T>() where T : class
        {
            return GetInstance<T, T>();
        }

        public static object GetInstance(Type type)
        {
            return TinyIoCContainer.Current.Resolve(type);
        }
    }
}