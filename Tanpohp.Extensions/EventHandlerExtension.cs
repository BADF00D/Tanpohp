#region usings

using System;
using Tanpohp.Annotations.Resharper;

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
        public static void CheckedInvoke(this EventHandler handler, [NotNull]object sender)
        {
            if (handler != null) handler(sender, EventArgs.Empty);
        }

        /// <summary>
        /// Invokes handler with EventArgs.Empty after checking if listeners exists.
        /// </summary>
        /// <param name="handler">EventHandler to invoke.</param>
        /// <param name="sender">Sender that invokes event. Usually "this".</param>
        /// <param name="createArgs">Callback that creates arguments handler is invoked with. This is only called when handler can be invoked.</param>
        public static void CheckedInvoke(this EventHandler handler, [NotNull]object sender, [NotNull]Func<EventArgs> createArgs)
        {
            if (handler != null) handler(sender, createArgs());
        }

        /// <summary>
        /// Invokes handler with EventArgs.Empty after checking if listeners exists.
        /// </summary>
        /// <param name="handler">EventHandler to invoke.</param>
        /// <param name="sender">Sender that invokes event. Usually "this".</param>
        /// <param name="args">Arguments the handler should be invoked with.</param>
        public static void CheckedInvoke<T>(this EventHandler<T> handler, [NotNull]object sender, [NotNull]T args)
        {
            if (handler != null) handler(sender, args);
        }

        /// <summary>
        /// Invokes handler with EventArgs.Empty after checking if listeners exists.
        /// </summary>
        /// <param name="handler">EventHandler to invoke.</param>
        /// <param name="sender">Sender that invokes event. Usually "this".</param>
        /// <param name="createArgs">Callback that creates arguments handler is invoked with. This is only called when handler can be invoked.</param>
        public static void CheckedInvoke<T>(this EventHandler<T> handler, [NotNull]object sender, [NotNull]Func<T> createArgs) where T : EventArgs
        {
            if (handler != null) handler(sender, createArgs());
        }
    }
}
