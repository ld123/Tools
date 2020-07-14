using Common.Common;
using System.Diagnostics;

namespace Common.Utilities
{
    public class Performance : DisposeBase
    {
        private readonly string _prefix;
        private readonly bool _printMemory;
        private readonly Stopwatch _stopwatch;
        private readonly long _useMemory;

        public Performance(string prefix, bool printMemory = false)
        {
            _prefix = prefix;
            _printMemory = printMemory;

            _stopwatch = new Stopwatch();
            _stopwatch.Start();
            Logger.Info($"{prefix} Start.");
            if (printMemory)
            {
                _useMemory = Utility.GetUseMemory();
            }

            Logger.Indent();
        }

        public override void Dispose()
        {
            var elapsed = _stopwatch.ElapsedMilliseconds;
            _stopwatch.Stop();
            Logger.UnIndent();

            var log = $"{_prefix} End. Consume Time: {elapsed} ms";
            if (_printMemory)
            {
                var current = Utility.GetUseMemory();
                var memory = current - _useMemory;
                log += $", Consume Memory: {memory} kb";
            }

            Logger.Info(log);

            base.Dispose();
        }
    }
}