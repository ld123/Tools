using Common.Enums;
using Common.Utilities;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

// ReSharper disable UnusedMember.Global

namespace Common.Common
{
    public static class Logger<T>
    {
        /// <summary>
        /// 日志处理事件
        /// 返回值为是否已经处理，不再需要后续输出
        /// </summary>
        public static event Func<T, LogLevel, bool> LogHandle;

        internal static bool Log(T obj, LogLevel level)
        {
            return LogHandle?.Invoke(obj, level) ?? false;
        }
    }

    public static class Logger
    {
        public static bool Detail { get; set; }

        public static int IndentLevel
        {
            get { return Trace.IndentLevel; }
        }

        public static void Fatal<T>(T obj, [CallerMemberName] string name = null, [CallerFilePath] string path = null,
            [CallerLineNumber] int line = 0)
        {
            if (obj == null) return;

            var handler = Logger<T>.Log(obj, LogLevel.Fatal);
            if (handler) return;

            var msg = obj.ToString();
            var detail = $"{msg}\npath:{path}-line:{line}, name:{name}";
            Trace.Assert(false, msg, detail);
        }

        public static void Error<T>(T obj)
        {
            if (obj == null) return;

            var handler = Logger<T>.Log(obj, LogLevel.Error);
            if (handler) return;

            Trace.TraceError(obj.ToString());
        }

        public static void Warning<T>(T obj)
        {
            if (obj == null) return;

            var handler = Logger<T>.Log(obj, LogLevel.Warning);
            if (handler) return;

            Trace.TraceWarning(obj.ToString());
        }

        public static void Info<T>(T obj)
        {
            if (obj == null) return;

            var handler = Logger<T>.Log(obj, LogLevel.Info);
            if (handler) return;

            Trace.TraceInformation(obj.ToString());
        }

        public static void Force<T>(T obj)
        {
            if (obj == null) return;

            var handler = Logger<T>.Log(obj, LogLevel.Force);
            if (handler) return;

            Trace.WriteLine(obj.ToString());
        }

        public static void Debug<T>(T obj)
        {
            if (obj == null) return;

            var handler = Logger<T>.Log(obj, LogLevel.Debug);
            if (handler) return;

            if (Utility.IsAttach || Utility.IsDebug)
            {
                Trace.WriteLine(obj.ToString());
            }
        }

        public static void Indent()
        {
            Trace.Indent();
        }

        public static void UnIndent()
        {
            Trace.Unindent();
        }
    }
}