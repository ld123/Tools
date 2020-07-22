using Common.Commands;
using Common.ViewModels;
using System;
using System.Windows.Input;
using WpfApp3.Commons;
using WpfApp3.Utilitys;

namespace WpfApp3.ViewModels
{
    public class MainWindowViewModel : ViewModelBase, IMainWindowViewModel
    {
        private MainPage _currentPage = MainPage.None;

        public MainPage CurrentPage
        {
            get { return _currentPage; }
            set { RaisePropertyIfChanged(ref _currentPage, value); }
        }

        private bool _showMask;

        public bool ShowMask
        {
            get { return _showMask; }
            set { RaisePropertyIfChanged(ref _showMask, value); }
        }

        public ICommand PageChangeCommand { get; }

        public MainWindowViewModel()
        {
            PageChangeCommand = new ActionCommand("更改主页", OnPageChangeCommandExecuted);
        }

        private void OnPageChangeCommandExecuted(ActionCommand command, object param)
        {
            if (!(param is MainPage page)) return;
            if (page == _currentPage) return;
            CurrentPage = page;
            Utility.ClearMemory(1024 * 1024 * 108); // 内存大于108MB时则回收
//            CurrentPage = ChangePage(_currentPage);
        }

        private static MainPage ChangePage(MainPage page)
        {
            var current = (int) page;
            var values = Enum.GetValues(typeof(MainPage));
            var count = values.Length;
            int? start = null;
            foreach (var temp in values)
            {
                var value = (int) temp;
                if (start != null && !(start > value)) continue;
                start = value;
            }

            var next = (MainPage) ((current + 1) % count + start);
            return next;
        }
    }
}