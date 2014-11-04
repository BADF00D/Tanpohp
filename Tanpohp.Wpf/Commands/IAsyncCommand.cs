#region usings

using System.Windows.Input;

#endregion

namespace Tanpohp.Wpf.Commands
{
	public interface IAsyncCommand : ICommand
	{
		/// <summary>
		/// Cancels excecut
		/// </summary>
		void Cancel();
	}
}