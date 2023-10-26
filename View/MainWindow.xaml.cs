using System.Windows;
using CoinTrack.ViewModel;
using static System.Windows.WindowState;

namespace CoinTrack.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Fixing maximized window overlapping the taskbar
            MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight - 2;
            MaxWidth = SystemParameters.MaximizedPrimaryScreenWidth;

            Minimize.Click += delegate { WindowState = Minimized; };
            Maximize.Click += delegate { WindowState = WindowState == Maximized ? Normal : Maximized; };
            CloseWindow.Click += delegate { Close(); };

            var context = new MainViewModel();

            DataContext = context;
            context.FetchData.Execute(this);
        }
    }
}