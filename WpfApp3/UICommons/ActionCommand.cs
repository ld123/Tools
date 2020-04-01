using System;

namespace WpfApp3.UICommons
{
    public class ActionCommand : CommandBase
    {
        public ActionCommand(string name, Action<object> action) : base(name, action, null)
        {
        }
    }
}