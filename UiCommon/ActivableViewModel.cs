using System;
using System.Diagnostics.CodeAnalysis;

namespace UiCommon
{
    [SuppressMessage("ReSharper", "IdentifierTypo")]
    public class ActivableViewModel : ViewModelBase
    {
        public event Func<bool, bool, bool> IsActiveChanging;
        public event Action<bool> IsActiveChanged;

        public bool Performance { get; set; }

        private bool _isActive;

        public bool IsActive
        {
            get { return _isActive; }
            set
            {
                if (!OnIsActiveChanging(_isActive, value)) return;
                if (!RaisePropertyIfChanged(ref _isActive, value)) return;
                OnIsActiveChanged(value);
            }
        }

        private bool OnIsActiveChanging(bool old, bool value)
        {
            return IsActiveChanging?.Invoke(old, value) ?? true;
        }

        private void OnIsActiveChanged(bool value)
        {
            var handle = IsActiveChanged;
            if (handle == null) return;

            handle.Invoke(value);
        }
    }
}