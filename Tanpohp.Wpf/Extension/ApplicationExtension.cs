#region usings

using System;
using System.Windows;

#endregion

namespace Tanpohp.Wpf.Extension
{
	public static class ApplicationExtension
	{
		public static void DispatchInGuiThread(this Application application, Delegate method)
		{
			if (application == null) return;
			application.Dispatcher.BeginInvoke(method);
		}
	}
}