using System;
using System.Windows;
using WpfApp3.Commons;

namespace WpfApp3.Controls
{
    /// <summary>
    /// TestWindow.xaml 的交互逻辑
    /// </summary>
    public partial class TestWindow : Window, ITestWindow
    {
        public TestWindow()
        {
            InitializeComponent();
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            GC.Collect();
        }
    }
}