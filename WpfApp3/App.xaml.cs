using System;
using System.Windows;
using System.Windows.Threading;
using TinyIoC;
using UiCommon.Converters;
using WpfApp3.Commons;
using WpfApp3.Controls;
using WpfApp3.Converters;
using WpfApp3.ViewModels;

namespace WpfApp3
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Application.Current.DispatcherUnhandledException += OnDispatcherUnHandleException;
            AppDomain.CurrentDomain.UnhandledException += OnCurrentDomainUnhandledException;
        }

        private void OnCurrentDomainUnhandledException(object sender, System.UnhandledExceptionEventArgs e)
        {
            var target = e.ExceptionObject is Exception ex ? ex : e.ExceptionObject;
            Console.WriteLine($"UnhandledException:{target}");
        }

        private void OnDispatcherUnHandleException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            Console.WriteLine($"DispatcherUnhandledException:{e.Exception}");
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var container = TinyIoCContainer.Current;

            container.Register<ITestWindow, TestWindow>().AsMultiInstance();
            container.Register<ICalc, Win10CalcControl>().AsMultiInstance();
            container.Register<ITest, TestControl>().AsMultiInstance();

            container.Register<PageEnumToControlConverter, PageEnumToControlConverter>().AsSingleton();
            container.Register<EnumToDescriptionConverter, EnumToDescriptionConverter>().AsSingleton();

            container.Register<IMainWindow, MainWindow>().AsSingleton();
            container.Register<IMainWindowViewModel, MainWindowViewModel>().AsSingleton();

            container.AutoRegister();
        }
    }
}