using System;
using System.Windows;
using System.Windows.Input;

namespace UiCommon.Models
{
    public class EventSetterInput : Freezable
    {
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register(nameof(Command), typeof(ICommand), typeof(EventSetterInput),
                new PropertyMetadata(default(ICommand)));

        public ICommand Command
        {
            get { return (ICommand) GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.Register(nameof(CommandParameter), typeof(object), typeof(EventSetterInput),
                new PropertyMetadata(default(object)));

        public object CommandParameter
        {
            get { return (object) GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        public object Sender { get; private set; }

        public EventArgs Args { get; private set; }

        internal void SetSenderInfo(object sender, EventArgs args)
        {
            Sender = sender;
            Args = args;
        }

        protected override Freezable CreateInstanceCore()
        {
            return new EventSetterInput();
        }
    }
}