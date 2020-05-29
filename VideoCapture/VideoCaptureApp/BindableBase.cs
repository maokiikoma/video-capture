﻿using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace VideoCaptureApp
{
    public class BindableBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual bool SetProperty<T>(ref T field, T value, 
            [CallerMemberName]string propertyName = null)
        {
            if (Equals(field, value)) { return false; }
            field = value;
            var h = this.PropertyChanged;
            if (h != null) { h(this, new PropertyChangedEventArgs(propertyName)); }
            return true;
        }
    }
}
