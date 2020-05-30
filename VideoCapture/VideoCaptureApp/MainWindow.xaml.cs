using MahApps.Metro.Controls;
using System.Windows;
using VideoCaptureApp.ViewModel;

namespace VideoCaptureApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow(MainWindowViewModel viewModel)
        {
            InitializeComponent();

            base.DataContext = viewModel;
        }
    }
}
