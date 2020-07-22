using Common.Common;
using Common.Utilities;
using System;

namespace Common.Commands
{
    public class CheckableCommand : ActionCommand<CheckableCommand>
    {
        public event Func<CheckableCommand, bool, bool> IsCheckChangingEvent;
        public event Action<CheckableCommand, bool> IsCheckChangedEvent;

        private bool _isChecked;

        public bool IsChecked
        {
            get { return _isChecked; }
            set
            {
                if (_isChecked == value) return;
                using (new Performance($"{Prefix} IsChecked Change"))
                {
                    if (!OnIsCheckChanging(value))
                    {
                        Logger.Info($"{Prefix} IsChecked Canceled");
                        return;
                    }

                    RaisePropertyChanged(ref _isChecked, value);
                    OnIsCheckChanged(value);
                }
            }
        }

        public CheckableCommand(string name, Action<CheckableCommand, object> executedAction,
            Func<CheckableCommand, object, bool> canExecutedFunc = null)
            : base(name, executedAction, canExecutedFunc)
        {
        }

        protected override void ExecuteBefore(object parameter)
        {
            IsChecked = true;
            base.ExecuteBefore(parameter);
        }

        protected override bool CanExecuteCore(object parameter)
        {
            if (!IsChecked) return false;
            return base.CanExecuteCore(parameter);
        }

        protected virtual bool OnIsCheckChanging(bool value)
        {
            return IsCheckChangingEvent?.Invoke(this, value) ?? true;
        }

        protected virtual void OnIsCheckChanged(bool value)
        {
            IsCheckChangedEvent?.Invoke(this, value);
        }
    }
}