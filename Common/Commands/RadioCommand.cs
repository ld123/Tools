using System;
using System.Collections.Generic;

namespace Common.Commands
{
    public class RadioCommand : CheckableCommand
    {
        private static readonly Dictionary<string, List<WeakReference<RadioCommand>>> Dictionary =
            new Dictionary<string, List<WeakReference<RadioCommand>>>();

        public string Group { get; }

        public RadioCommand(string name, string group, Action<CheckableCommand, object> executedAction,
            Func<CheckableCommand, object, bool> canExecutedFunc = null) :
            base(name, executedAction, canExecutedFunc)
        {
            AddReference(Dictionary, group);
            Group = group;
        }

        protected override void OnIsCheckChanged(bool value)
        {
            base.OnIsCheckChanged(value);

            if (value)
            {
                SetOtherDisabled(Dictionary[Group]);
            }
        }

        private void AddReference(IDictionary<string, List<WeakReference<RadioCommand>>> dictionary, string key)
        {
            if (dictionary.TryGetValue(key, out var list))
            {
                list.Add(new WeakReference<RadioCommand>(this));
            }
            else
            {
                dictionary[key] = new List<WeakReference<RadioCommand>> {new WeakReference<RadioCommand>(this)};
            }
        }

        private void SetOtherDisabled(IList<WeakReference<RadioCommand>> list)
        {
            for (int i = list.Count - 1; i >= 0; i--)
            {
                var reference = list[i];
                if (!reference.TryGetTarget(out var item))
                {
                    list.Remove(reference);
                    continue;
                }

                if (item == this) continue;
                item.IsChecked = false;
            }
        }
    }
}