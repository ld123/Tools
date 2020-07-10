using System.Diagnostics;

namespace Common.Utility
{
    public class Utility
    {
#if DEBUG
        public static bool IsDebug { get; } = true;
#else
        public static bool IsDebug { get; } = false;
#endif

        public static bool IsAttach { get; } = Debugger.IsAttached;
    }
}