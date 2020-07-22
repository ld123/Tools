using System.ComponentModel;

namespace WpfApp3.Commons
{
    public enum MainPage
    {
        [Description("无")] None,
        [Description("计算器")] Calc,
        [Description("测试")] Test,
        [Description("无限移动1")] InfiniteMove1,
        [Description("无限移动2")] InfiniteMove2,
    }
}