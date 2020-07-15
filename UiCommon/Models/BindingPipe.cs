using System.Windows;
using UiCommon.Utilities;

namespace UiCommon.Models
{
    public class BindingPipe : Freezable
    {
        #region Source (DependencyProperty)

        public object Source
        {
            get { return (object) GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register("Source", typeof(object), typeof(BindingPipe),
                new FrameworkPropertyMetadata(null, new PropertyChangedCallback(OnSourceChanged)));

        private static void OnSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (UiUtility.IsInDesign) return;
            ((BindingPipe) d).OnSourceChanged(e);
        }

        protected virtual void OnSourceChanged(DependencyPropertyChangedEventArgs e)
        {
            Target = e.NewValue;
        }

        #endregion

        #region Target (DependencyProperty)

        public object Target
        {
            get { return (object) GetValue(TargetProperty); }
            set { SetValue(TargetProperty, value); }
        }

        public static readonly DependencyProperty TargetProperty =
            DependencyProperty.Register("Target", typeof(object), typeof(BindingPipe),
                new FrameworkPropertyMetadata(null));

        #endregion

        protected override Freezable CreateInstanceCore()
        {
            return new BindingPipe();
        }
    }
}