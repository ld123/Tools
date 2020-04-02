using WpfApp3.UICommons;

namespace WpfApp3.Commons
{
    public interface ITestBase
    {
    }

    public interface ITest : ITestBase
    {
    }

    public interface ITestWindow : ITestBase
    {
    }

    public interface IMainWindow : ITestBase
    {
    }

    public interface ICalc : ITestBase
    {
    }

    public interface IMainWindowViewModel : ITestBase
    {
        MainPage CurrentPage { get; set; }

        ActionCommand PageChangeCommand { get; }

        bool ShowMask { get; set; }
    }
}