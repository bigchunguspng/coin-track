using System.Windows;
using CoinTrack.ViewModel;

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

            var vm = new MainViewModel();
            
            DataContext = vm;
            vm.FetchData.Execute(this);
        }
    }
}
