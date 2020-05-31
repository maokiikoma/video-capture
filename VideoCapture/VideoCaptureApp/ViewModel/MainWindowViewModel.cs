using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows;
using Lib;
using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;
using VideoCaptureApp.Services;
using Forms = System.Windows.Forms;

namespace VideoCaptureApp.ViewModel
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly IVideoCaptureService _videoCaptureService;

        #region property

        private string _fileName;
        public string FileName
        {
            get { return _fileName; }
            set { SetProperty(ref _fileName, value); }
        }

        private string _outPath = @"C:\Users\masaaoki\Pictures\capture";
        public string OutPath
        {
            get { return _outPath; }
            set { SetProperty(ref _outPath, value); }
        }

        private string _interval = "200";
        public string Interval
        {
            get { return _interval; }
            set { SetProperty(ref _interval, value); }
        }

        private string _errorMessage;
        public string ErrorMessage
        {
            get { return _errorMessage; }
            set { SetProperty(ref _errorMessage, value); }
        }

        private bool _lodingIsActive = false;
        public bool LodingIsActive
        {
            get { return _lodingIsActive; }
            set { SetProperty(ref _lodingIsActive, value); }
        }

        private Visibility _overRayVisibility = Visibility.Collapsed;
        public Visibility OverRayVisibility
        {
            get { return _overRayVisibility; }
            set { SetProperty(ref _overRayVisibility, value); }
        }

        #endregion

        #region command

        private RelayCommand _fileSelectCommand;
        public RelayCommand FileSelectCommand
        {
            get { return _fileSelectCommand = _fileSelectCommand ?? new RelayCommand(ExecuteFileSelect); }
        }

        private RelayCommand _folderSelectCommand;
        public RelayCommand FolderSelectCommand
        {
            get { return _folderSelectCommand = _folderSelectCommand ?? new RelayCommand(ExecuteFolderSelect); }
        }

        private RelayCommand _startCommand;
        public RelayCommand StartCommand
        {
            get { return _startCommand = _startCommand ?? new RelayCommand(ExecuteStart); }
        }

        #endregion


        public MainWindowViewModel(IVideoCaptureService videoCaptureService)
        {
            _videoCaptureService = videoCaptureService;
        }



        #region private

        private void ExecuteFileSelect()
        {
            var dialog = new OpenFileDialog { Title = "ファイルを開く", Filter = "全てのファイル(*.*)|*.*" };
            if (dialog.ShowDialog() == true)
            {
                FileName = dialog.FileName;
            }
        }

        private void ExecuteFolderSelect()
        {
            var dlg = new Forms.FolderBrowserDialog();
            dlg.Description = "フォルダーを選択してください。";

            if (dlg.ShowDialog() == Forms.DialogResult.OK)
            {
                OutPath = dlg.SelectedPath;
            }
        }

        private void ExecuteStart()
        {
            ToggleProgressRing(); // TODO aoki メッセージボックスを出さないとローディングが表示されない
            ErrorMessage = null;

            var (validResult, errorMessage) = _videoCaptureService.Validate(FileName, OutPath, Interval);
            if (!validResult)
            {
                DoErrorProc(errorMessage, "( ﾉД`)");

                ToggleProgressRing();

                return;
            }

            //ToggleProgressRing();

            var result = _videoCaptureService.Capture(FileName, OutPath, Interval);
            if (result.Result)
            {
                MessageBox.Show("正常終了しました。", "(^O^)", MessageBoxButton.OK, MessageBoxImage.Information);
                Process.Start("explorer.exe", OutPath);
            }
            else
            {
                DoErrorProc(result.ErrorMessage, "(T ^ T)");
            }

            ToggleProgressRing();
        }

        private void DoErrorProc(string errorMessage, string caption)
        {
            MessageBox.Show(errorMessage, caption, MessageBoxButton.OK, MessageBoxImage.Warning);
            ErrorMessage = errorMessage;
        }

        /// <summary>
        /// 処理中イメージの表示／非表示切り替え
        /// 
        /// </summary>
        private void ToggleProgressRing()
        {
            if (LodingIsActive)
            {
                LodingIsActive = false;
                OverRayVisibility = Visibility.Collapsed;
            }
            else
            {
                LodingIsActive = true;
                OverRayVisibility = Visibility.Visible;
            }
        }

        #endregion
    }
}
