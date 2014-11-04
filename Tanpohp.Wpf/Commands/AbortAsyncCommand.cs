using System;
using System.Windows;
using System.Windows.Input;
using Tanpohp.Annotations.Resharper;
using Tanpohp.Extensions;
using ApplicationExtension = Tanpohp.Wpf.Extension.ApplicationExtension;

namespace Tanpohp.Wpf.Commands
{
	/// <summary>
	/// Class is able to cancel an IAsyncCommand. 
	/// </summary>
	/// <remarks>This command is always excecuteable. If you like to change this, override virtual CanExcecute().</remarks>
	public class AbortAsyncCommand : ICommand
	{
		private readonly IAsyncCommand _asyncCommand;

		private readonly Action _invokeCanExcecuteChangedAction;

		public AbortAsyncCommand([NotNull]IAsyncCommand asyncCommand)
		{
			_asyncCommand = asyncCommand;
			_invokeCanExcecuteChangedAction = new Action(() => CanExecuteChanged.CheckedInvoke(this));
		}

		public void Execute(object parameter)
		{
			_asyncCommand.Cancel();
		}

		public virtual bool CanExecute(object parameter)
		{
			return true;
		}

		public event EventHandler CanExecuteChanged;

		protected void InvokeCanExecuteChanged()
		{
			ApplicationExtension.DispatchInGuiThread(Application.Current, _invokeCanExcecuteChangedAction);
		}
	}
}