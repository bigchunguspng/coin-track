using System;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Threading;

namespace CoinTrack
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;

            Dispatcher.UnhandledException += OnUnhandledException;

            AppDomain.CurrentDomain.ProcessExit += delegate
            {
                var directory = new DirectoryInfo(Directory.GetCurrentDirectory());

                foreach (var file in directory.GetFiles("chart.*.png"))
                {
                    try
                    {
                        file.Delete();
                    }
                    catch
                    {
                        // do nothing
                    }
                }
            };
        }

        private void OnUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show(e.Exception.Message, "Oops…", MessageBoxButton.OK, MessageBoxImage.Error);
            e.Handled = true;
        }
    }
}