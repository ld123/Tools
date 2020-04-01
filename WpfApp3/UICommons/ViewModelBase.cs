using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WpfApp3.UICommons
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        protected bool RaisePropertyIfChanged<T>(ref T filed, T value, [CallerMemberName] string name = null)
        {
            if (Equals(filed, value)) return false;
            filed = value;
            OnPropertyChanged(name);
            return true;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}