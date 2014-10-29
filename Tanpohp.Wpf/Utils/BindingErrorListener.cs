#region usings

using System;
using System.Diagnostics;
using System.Windows;

#endregion

namespace Tanpohp.Wpf.Utils
{
    /// <summary>
    /// Listens to binding errors as inspired from here: http://stackoverflow.com/questions/4225867/how-can-i-turn-binding-errors-into-runtime-exceptions
    /// </summary>
    public class BindingErrorListener : TraceListener
    {
        private Action<string> _logAction;

        public static void Listen(Action<string> logAction)
        {
            PresentationTraceSources.DataBindingSource.Listeners.Add(new BindingErrorListener {_logAction = logAction});
        }

        public static void HandleErrorsAsExceptions()
        {
            Listen(message => { throw new Exception(message); });
        }

        public static void DisplayErrorsInMessageBox()
        {
            Listen(message => MessageBox.Show(message));
        }

        public override void Write(string message)
        {
        }

        public override void WriteLine(string message)
        {
            _logAction(message);
        }
    }
}