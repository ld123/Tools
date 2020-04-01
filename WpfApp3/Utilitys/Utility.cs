using System;
using System.Diagnostics;

namespace WpfApp3.Utilitys
{
    public static class Utility
    {
        public static void ClearMemory(long limit = 0)
        {
            if (limit > 0 && GetUseMemory() <= limit) return;
            GC.Collect();
        }

        public static long GetUseMemory()
        {
            using (var process = Process.GetCurrentProcess())
            {
                var memory = (process.PrivateMemorySize64 / 1e+6) * 1024 * 1024;
                var size = (long) memory;
                return size;
            }
        }
    }
}