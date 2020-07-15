using Common.Common;

namespace Common.ViewModels
{
    public class ViewModelBase : NotifyObject
    {
        private string _name;

        public string Name
        {
            get { return _name; }
            set { RaisePropertyIfChanged(ref _name, value); }
        }
    }
}