using Common.Common;

namespace Common.ViewModels
{
    public class ViewModelBase : NotifyObject
    {
        public string Name { get; }

        protected readonly string Prefix;

        public ViewModelBase() : this(default)
        {
        }

        public ViewModelBase(string name)
        {
            Name = name;
            Prefix = $"{GetType().Name}({Name})";
        }
    }
}