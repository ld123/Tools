using System.ComponentModel;

namespace Common.Enums
{
    public enum LogLevel
    {
        /// <summary>
        /// 跟踪
        /// </summary>
        [Description("跟踪")]
        Trace,

        /// <summary>
        /// 调试
        /// </summary>
        [Description("调试")]
        Debug,

        /// <summary>
        /// 信息
        /// </summary>
        [Description("信息")]
        Info,

        /// <summary>
        /// 警告
        /// </summary>
        [Description("警告")]
        Warning,

        /// <summary>
        /// 错误
        /// </summary>
        [Description("错误")]
        Error,

        /// <summary>
        /// 致命
        /// </summary>
        [Description("致命")]
        Fatal,

        /// <summary>
        /// 关闭
        /// </summary>
        [Description("关闭")]
        Off,

        /// <summary>
        /// 强制
        /// </summary>
        [Description("强制")]
        Force,
    }
}