using System.Windows.Input;

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

    public interface IInfiniteMove1 : ITestBase
    {
    }

    public interface IInfiniteMove2 : ITestBase
    {
    }

    public interface IMainWindowViewModel : ITestBase
    {
        MainPage CurrentPage { get; set; }

        ICommand PageChangeCommand { get; }

        bool ShowMask { get; set; }
    }
}