using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Tanpohp.Extensions;

namespace Tanpohp.Wpf.Commands
{
	public abstract class AsyncCommand : IAsyncCommand
	{
		private CancellationTokenSource _cancellationTokenSource;

		private readonly object _lockKey = new object();

		public void Execute(object parameter)
		{
			lock (_lockKey)
			{
				_cancellationTokenSource = new CancellationTokenSource();
				InvokeCanExecuteChanged();
				Task.Factory.StartNew(() => PerformAsync(_cancellationTokenSource.Token)).ContinueWith(
					delegate
					{
						lock (_lockKey)
						{
							_cancellationTokenSource = null;
							InvokeCanExecuteChanged();
						}
					});
				
			}
		}

		/// <summary>
		/// This methods is called async. Must call 
		/// </summary>
		/// <param name="token">Token used to inform method of cancellation was requested.</param>
		protected abstract void PerformAsync(CancellationToken token);

		public virtual bool CanExecute(object parameter)
		{
			return _cancellationTokenSource == null;
		}

		public event EventHandler CanExecuteChanged;

		protected void InvokeCanExecuteChanged()
		{
			Application.Current.Dispatcher.BeginInvoke(new Action(() => CanExecuteChanged.CheckedInvoke(this)));
		}
		
		public void Cancel()
		{
			if (_cancellationTokenSource == null || _cancellationTokenSource.IsCancellationRequested) return;

			_cancellationTokenSource.Cancel();
		}
	}
}