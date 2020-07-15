using NLog;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace UiCommon
{
    public class NotifyObject : INotifyPropertyChanged
    {
        protected readonly Logger Logger;

        public NotifyObject()
        {
            var type = GetType();
            Logger = LogManager.GetCurrentClassLogger(type);
        }

        protected bool RaisePropertyIfChanged<T>(ref T filed, T value, [CallerMemberName] string name = null)
        {
            try
            {
                if (Equals(filed, value)) return false;
                return RaisePropertyChanged(ref filed, value, name);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        protected bool RaisePropertyChanged<T>(ref T filed, T value, [CallerMemberName] string name = null)
        {
            try
            {
                filed = value;
                OnPropertyChanged(name);
                return true;
            }
            catch (Exception e)
            {
                Logger.Error(e);
                return false;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            try
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }
    }
}