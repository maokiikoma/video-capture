using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Lib;
using Microsoft.Win32;

namespace VideoCaptureApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.Title = "ファイルを開く";
            dialog.Filter = "全てのファイル(*.*)|*.*";
            if (dialog.ShowDialog() == true)
            {
                this.textBoxFileName.Text = dialog.FileName;
            }
            else
            {
                this.textBoxFileName.Text = "キャンセルされました";
            }
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var lib = new VideoLib();
                var result = lib.Exec(this.textBoxFileName.Text, this.textBoxOutPath.Text, int.Parse(this.textBoxInterval.Text));
                if (result)
                {
                    MessageBox.Show("正常終了しました。", "message box", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("入力がおかしいです。", "message box", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                MessageBox.Show("エラーが発生しました。", "message box", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
