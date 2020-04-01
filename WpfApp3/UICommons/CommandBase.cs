using System;
using System.Windows.Input;

namespace WpfApp3.UICommons
{
    public class CommandBase : ViewModelBase, ICommand
    {
        public string Name { get; }

        private bool _isEnabled;

        public bool IsEnabled
        {
            get { return _isEnabled; }
            set { RaisePropertyIfChanged(ref _isEnabled, value); }
        }

        private bool _canExecuted = true;

        public bool CanExecuted
        {
            get { return _canExecuted; }
            set
            {
                if (!RaisePropertyIfChanged(ref _canExecuted, value)) return;
                ChangeCanExecute(false);
            }
        }

        public Action<object> ExecuteAction { get; }

        public Func<object, bool> CanExecuteAction { get; }

        public CommandBase(string name, Action<object> action, Func<object, bool> canExecuteAction)
        {
            Name = name;
            ExecuteAction = action;
            CanExecuteAction = canExecuteAction;
        }

        public bool CanExecute(object parameter)
        {
            var action = CanExecuteAction;
            if (action == null) return _canExecuted;
            return action.Invoke(parameter);
        }

        public void Execute(object parameter)
        {
            if (!CanExecute(parameter)) return;
            var start = DateTime.Now;
            Console.WriteLine($"{start:hh:mm:ss ffff} [{Name}]开始执行.");
            ExecuteAction?.Invoke(parameter);
            var now = DateTime.Now;
            Console.WriteLine($"{now:hh:mm:ss ffff} [{Name}]执行完成! {(now - start).TotalMilliseconds}ms.");
        }

        public event EventHandler CanExecuteChanged;

        private void ChangeCanExecute(bool can)
        {
            _canExecuted = can;
            CanExecuteChanged?.Invoke(this, new CanExecuteArgs(can));
        }
    }

    public class CanExecuteArgs : EventArgs
    {
        public bool CanExecute { get; }

        public CanExecuteArgs(bool canExecute)
        {
            CanExecute = canExecute;
        }
    }
}