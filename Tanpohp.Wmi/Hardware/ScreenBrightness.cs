#region usings

using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using Tanpohp.Extensions;

#endregion

namespace Tanpohp.Wmi.Hardware
{
    /// <summary>
    /// This class is able to set screen brightness.
    /// </summary>
    public class ScreenBrightness
    {
        private const string ManagementScopeName = "root\\WMI";

        private const string QuerySelector = "WmiMonitorBrightnessMethods";

        private const string SetBrightnessMethodName = "WmiSetBrightness";

        private const string BrightnessEventQuery = "Select * From WmiMonitorBrightnessEvent";

        private static ManagementEventWatcher _eventWatcher;

        /// <summary>
        /// Sets screen brightness to desired percent.
        /// </summary>
        /// <param name="b">ScreenBrightness in percent.</param>
        /// <remarks>Not all values between [0,100] are supported by each driver/monitor. So
        /// the driver uses ne nearest possible value.</remarks>
        /// <exception cref="ArgumentException">Thrown is b is bigger then 100.</exception>
        public static void SetScreenBrightness(byte b)
        {
            if(b > 100) throw new ArgumentException("Paremeter b must be [0,100]."); 

            var scope = new ManagementScope(ManagementScopeName);
            var query = new SelectQuery(QuerySelector);

            using (var searcher = new ManagementObjectSearcher(scope, query))
            {
                using (var objectCollection = searcher.Get())
                {
                    foreach (var managementObject in objectCollection.OfType<ManagementObject>())
                    {
                        managementObject.InvokeMethod(SetBrightnessMethodName, new object[] { UInt32.MaxValue, b });
                    }
                }
            }
        }

        private static readonly IList<EventHandler<BrightnessChangedEventArgs>> Handler = new List<EventHandler<BrightnessChangedEventArgs>>();

        public static event EventHandler<BrightnessChangedEventArgs> BrightnessChangedEvent
        {
            add
            {
                Handler.Add(value);
                if(Handler.Count == 1) RegsiterForWmiBrightnessChangedEvent();
            }
            remove
            {
                Handler.Remove(value);
                if (Handler.Count == 0) UnregisterWmiBrightnessChangedEvent();
            }
        }

        private static void UnregisterWmiBrightnessChangedEvent()
        {
            _eventWatcher.Stop();
            _eventWatcher.EventArrived -= WmiEventHandler;
            _eventWatcher.Dispose();
        }

        private static void RegsiterForWmiBrightnessChangedEvent()
        {
            var scope = new ManagementScope(ManagementScopeName);
            scope.Connect();

            _eventWatcher = new ManagementEventWatcher(scope, new EventQuery(BrightnessEventQuery));
            _eventWatcher.EventArrived += WmiEventHandler;
            _eventWatcher.Start();
        }

        private static void WmiEventHandler(object sender, EventArrivedEventArgs e)
        {
            var args = BrightnessChangedEventArgs.From(e);

            Handler.ForEach(h => h(null, args));
        }
    }
}
