using System.Windows;
using VideoCaptureApp.ViewModel;

namespace VideoCaptureApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(MainWindowViewModel viewModel)
        {
            InitializeComponent();

            base.DataContext = viewModel;
        }
    }
}
