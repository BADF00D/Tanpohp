#region usings

using System;

#endregion

namespace Tanpohp.Data.Event
{
    /// <summary>
    /// Defines the delegate used for firing a ProgressionEvent.
    /// </summary>
    /// <param name="sender">Sender that fires the event.</param>
    /// <param name="args">Argument of this event.</param>
    public delegate void ProgressionEventHandler(object sender, ProgressionEventArgs args);


    public class ProgressionEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes an new instance of ProgressionEventArgs.
        /// </summary>
        /// <param name="current">Current progression values. Default: 0.</param>
        /// <param name="upperBound">UpperBound of progression: Default: 1.</param>
        public ProgressionEventArgs(double current = 0, double upperBound = 1.0)
        {
            Current = current;
            UpperBound = upperBound;
        }

        /// <summary>
        /// Current value of progression. Usually smaller or equal UpperBound.
        /// </summary>
        public double Current { get; private set; }

        /// <summary>
        /// UpperBound of the work to be done.
        /// </summary>
        public double UpperBound { get; private set; }
    }
}