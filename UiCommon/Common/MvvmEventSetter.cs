using Common.Common;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Windows;
using UiCommon.Models;

namespace UiCommon.Common
{
    [SuppressMessage("ReSharper", "IdentifierTypo")]
    public class MvvmEventSetter : EventSetter
    {
        public EventSetterInput Input { get; set; }

        public new Delegate Handler
        {
            get { return base.Handler; }
            private set { base.Handler = value; }
        }

        public new RoutedEvent Event
        {
            get { return base.Event; }
            set
            {
                base.Event = value;
                SetEventHandler();
            }
        }

        private void SetEventHandler()
        {
            try
            {
                if (Event == null) throw new InvalidOperationException("请先设置事件!");
                const BindingFlags flag = BindingFlags.NonPublic | BindingFlags.Instance;
                var method = GetType().GetMethod(nameof(InputBindingEventSetterDelegate), flag);
                var ctor = Event.HandlerType.GetConstructor(new[] {typeof(object), typeof(IntPtr)});
                Handler = (Delegate) ctor.Invoke(new object[] {this, method.MethodHandle.GetFunctionPointer()});
            }
            catch (Exception e)
            {
                Logger.Error("InputEventSetter 注册路由事件失败!" + e);
            }
        }

        private void InputBindingEventSetterDelegate(object sender, dynamic e)
        {
            var input = Input;
            if (input == null) return;

            if (!(e is EventArgs))
            {
                var fatal = $"{sender} Event {Event.Name} Args Is Not EventArgs.";
                Logger.Fatal(fatal);
            }

            input.SetSenderInfo(sender, e);
            input.Command?.Execute(input);
        }
    }
}