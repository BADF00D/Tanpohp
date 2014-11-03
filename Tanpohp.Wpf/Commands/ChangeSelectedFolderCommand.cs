#region usings

using System;
using System.Windows.Forms;
using System.Windows.Input;
using Tanpohp.Wpf.ViewModel;

#endregion

namespace Tanpohp.Wpf.Commands
{
	public class ChangeSelectedFolderCommand : ICommand
	{
		private readonly IContainSelectedFolderViewModel _viewModel;

		public ChangeSelectedFolderCommand(IContainSelectedFolderViewModel viewModel)
		{
			_viewModel = viewModel;
		}

		#region ICommand Members

		public void Execute(object parameter)
		{
			var dialog = new FolderBrowserDialog
			             	{
			             		Description = "Select folder that contains all documents to parse.",
			             		ShowNewFolderButton = false
			             	};
			if (dialog.ShowDialog() == DialogResult.OK)
			{
				_viewModel.SelectedFolder = dialog.SelectedPath;
			}
		}

		public virtual bool CanExecute(object parameter)
		{
			return true;
		}

		public event EventHandler CanExecuteChanged;

		#endregion
	}
}