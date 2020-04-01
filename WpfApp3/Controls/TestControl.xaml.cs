using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using WpfApp3.Commons;

namespace WpfApp3.Controls
{
    /// <summary>
    /// TestControl.xaml 的交互逻辑
    /// </summary>
    public partial class TestControl : UserControl, ITest
    {
        public TestControl()
        {
            InitializeComponent();

            Tag = GetTag(1024* 1024);
        }

        private static object GetTag(int size)
        {
            var list = Enumerable.Range(0, size).Select(x => new byte[16]).ToList();
            return list;
        }
    }
}