using Common.Common;
using Common.Utilities;
using Common.ViewModels;
using System;
using System.Windows.Input;

namespace Common.Commands
{
    public abstract class CommandBase : ViewModelBase, ICommand
    {
        public event EventHandler CanExecuteChanged;

        protected abstract bool CanExecuteCore(object parameter);
        protected abstract void ExecuteCore(object parameter);

        private bool _isEnabled = true;

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
                CanExecuteChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        private object _tag;

        public object Tag
        {
            get { return _tag; }
            set { RaisePropertyIfChanged(ref _tag, value); }
        }

        private string _text;

        public string Text
        {
            get { return _text; }
            set { RaisePropertyIfChanged(ref _text, value); }
        }

        private string _description;

        public string Description
        {
            get { return _description; }
            set { RaisePropertyIfChanged(ref _description, value); }
        }

        private string _tip;

        public string Tip
        {
            get { return _tip; }
            set { RaisePropertyIfChanged(ref _tip, value); }
        }

        protected CommandBase(string name) : this(name, default)
        {
        }

        protected CommandBase(string name, object tag) : base(name)
        {
            Text = name;
            Tag = tag;
        }

        public bool CanExecute(object parameter)
        {
            if (!IsEnabled) return false;
            return CanExecuteCore(parameter) && _canExecuted;
        }

        public void Execute(object parameter)
        {
            using (new Performance($"{Prefix} Execute"))
            {
                ExecuteBefore(parameter);
                if (!CanExecute(parameter))
                {
                    Logger.Info($"{Prefix} Can't Executed.");
                    return;
                }

                ExecuteCore(parameter);
            }
        }

        protected virtual void ExecuteBefore(object parameter)
        {
        }
    }
}