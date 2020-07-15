using System.Diagnostics;

namespace Common.Utilities
{
    public class Utility
    {
#if DEBUG
        public static bool IsDebug { get; } = true;
#else
        public static bool IsDebug { get; } = false;
#endif

        public static bool IsAttach { get; } = Debugger.IsAttached;

        public static long GetUseMemory()
        {
            using (var process = Process.GetCurrentProcess())
            {
                var memory = process.PrivateMemorySize64 / 1e+6 * 1024 * 1024;
                var size = (long) memory;
                return size;
            }
        }
    }
}