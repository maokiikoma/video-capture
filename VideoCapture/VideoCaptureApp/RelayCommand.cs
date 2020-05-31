using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace VideoCaptureApp
{
    public class RelayCommand : ICommand
    {
        private readonly Func<object, Task> _asyncAction;

        //Command実行時に実行するアクション、引数を受け取りたい場合はこのActionをAction<object>などにする
        private Action _action;

        public RelayCommand(Action action)
        {
            _action = action;
        }

        public RelayCommand(Func<object, Task> asyncAction)
        {
            _asyncAction = asyncAction;
        }

        #region ICommandインターフェースの必須実装

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return _action != null;
        }

        public void Execute(object parameter)
        {
            _action?.Invoke();
        }

        public Task ExecuteAsync(object parameter)
        {
            return _asyncAction(parameter);
        }

        #endregion
    }
}
