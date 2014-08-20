#region usings

using System;

#endregion

namespace Tanpohp.Extensions
{
    public static class EventHandlerExtension
    {
        /// <summary>
        /// Invokes handler with EventArgs.Empty after checking if listeners exists.
        /// </summary>
        /// <param name="handler">EventHandler to invoke.</param>
        /// <param name="sender">Sender that invokes event. Usually "this".</param>
        public static void CheckedInvoke(this EventHandler handler, object sender)
        {
            if (handler != null) handler(sender, EventArgs.Empty);
        }
    }
}
