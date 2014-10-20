#region usings

using System;
using System.Windows.Input;

#endregion

namespace Tanpohp.Wpf.Commands
{
    public abstract class ACanAlwaysExcecuteCommand : ICommand
    {
        #region ICommand Members

        public abstract void Execute(object parameter);

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        #endregion
    }
}