using System.ComponentModel;
using System.Windows;

namespace UiCommon.Utilities
{
    public static class UiUtility
    {
        public static bool IsInDesign => (bool) DesignerProperties.IsInDesignModeProperty
            .GetMetadata(typeof(DependencyObject)).DefaultValue;
    }
}