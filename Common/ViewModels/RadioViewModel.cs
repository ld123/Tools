using System;
using System.Collections.Generic;

namespace Common.ViewModels
{
    public class RadioViewModel : ActivableViewModel
    {
        private static readonly Dictionary<string, List<WeakReference<RadioViewModel>>> Dictionary =
            new Dictionary<string, List<WeakReference<RadioViewModel>>>();

        public string Group { get; }

        public RadioViewModel(string group)
        {
            Group = group;
            AddReference(Dictionary, group);
        }

        protected override void OnIsActiveChanged(bool value)
        {
            base.OnIsActiveChanged(value);

            if (value)
            {
                SetOtherDisabled(Dictionary[Group]);
            }
        }

        private void AddReference(IDictionary<string, List<WeakReference<RadioViewModel>>> dictionary, string key)
        {
            if (dictionary.TryGetValue(key, out var list))
            {
                list.Add(new WeakReference<RadioViewModel>(this));
            }
            else
            {
                dictionary[key] = new List<WeakReference<RadioViewModel>> {new WeakReference<RadioViewModel>(this)};
            }
        }

        private void SetOtherDisabled(IList<WeakReference<RadioViewModel>> list)
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
                item.IsActive = false;
            }
        }
    }
}