using Common.Common;
using Common.Utilities;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Common.ViewModels
{
    [SuppressMessage("ReSharper", "IdentifierTypo")]
    public class ActivableViewModel : ViewModelBase
    {
        public event Func<ActivableViewModel, bool, bool> IsActiveChanging;
        public event Action<ActivableViewModel, bool> IsActiveChanged;

        private bool _isActive;

        public bool IsActive
        {
            get { return _isActive; }
            set
            {
                if (_isActive == value) return;
                using (new Performance("IsActive Change"))
                {
                    if (!OnIsActiveChanging(value))
                    {
                        Logger.Info("IsActive Canceled");
                        return;
                    }

                    RaisePropertyChanged(ref _isActive, value);
                    OnIsActiveChanged(value);
                }
            }
        }

        protected virtual bool OnIsActiveChanging(bool value)
        {
            return IsActiveChanging?.Invoke(this, value) ?? true;
        }

        protected virtual void OnIsActiveChanged(bool value)
        {
            IsActiveChanged?.Invoke(this, value);
        }
    }
}