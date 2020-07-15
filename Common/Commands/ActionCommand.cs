using System;

namespace Common.Commands
{
    public abstract class ActionCommand<T> : CommandBase where T : ActionCommand<T>
    {
        public event Action<T, object> ExecutedEvent;
        public event Func<T, object, bool> CanExecuteEvent;

        protected ActionCommand(string name,
            Action<T, object> executedAction,
            Func<T, object, bool> canExecutedFunc = null)
            : base(name)
        {
            ExecutedEvent = executedAction;
            CanExecuteEvent = canExecutedFunc;
        }

        protected override bool CanExecuteCore(object parameter)
        {
            // 没有事件则默认为可执行
            // 无try-catch的强转性能比is和as好
            return CanExecuteEvent?.Invoke((T) this, parameter) ?? true;
        }

        protected override void ExecuteCore(object parameter)
        {
            // 无try-catch的强转性能比is和as好
            ExecutedEvent?.Invoke((T) this, parameter);
        }
    }

    public class ActionCommand : ActionCommand<ActionCommand>
    {
        public ActionCommand(string name, Action<ActionCommand, object> executedAction,
            Func<ActionCommand, object, bool> canExecutedFunc = null)
            : base(name, executedAction, canExecutedFunc)
        {
        }
    }
}