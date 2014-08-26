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

        private readonly IList<EventHandler<BrightnessChangedEventArgs>> _handler =
            new List<EventHandler<BrightnessChangedEventArgs>>();

        private ManagementEventWatcher _eventWatcher;

        /// <summary>
        /// Sets screen brightness to desired percent.
        /// </summary>
        /// <param name="b">ScreenBrightness in percent.</param>
        /// <remarks>Not all values between [0,100] are supported by each driver/monitor. So
        /// the driver uses ne nearest possible value.</remarks>
        /// <exception cref="ArgumentException">Thrown is b is bigger then 100.</exception>
        public void SetScreenBrightness(byte b)
        {
            if (b > 100) throw new ArgumentException("Parameter b must be [0,100].");

            var scope = new ManagementScope(ManagementScopeName);
            var query = new SelectQuery(QuerySelector);

            using (var searcher = new ManagementObjectSearcher(scope, query))
            {
                using (var objectCollection = searcher.Get())
                {
                    foreach (var managementObject in objectCollection.OfType<ManagementObject>())
                    {
                        managementObject.InvokeMethod(SetBrightnessMethodName, new object[] {UInt32.MaxValue, b});
                    }
                }
            }
        }

        public event EventHandler<BrightnessChangedEventArgs> BrightnessEvent
        {
            add
            {
                _handler.Add(value);
                if (_handler.Count == 1) RegsiterForWmiBrightnessChangedEvent();
            }
            remove
            {
                _handler.Remove(value);
                if (_handler.Count == 0) UnregisterWmiBrightnessChangedEvent();
            }
        }

        private void UnregisterWmiBrightnessChangedEvent()
        {
            _eventWatcher.Stop();
            _eventWatcher.EventArrived -= WmiEventHandler;
            _eventWatcher.Dispose();
        }

        private void RegsiterForWmiBrightnessChangedEvent()
        {
            var scope = new ManagementScope(ManagementScopeName);
            scope.Connect();

            _eventWatcher = new ManagementEventWatcher(scope, new EventQuery(BrightnessEventQuery));
            _eventWatcher.EventArrived += WmiEventHandler;
            _eventWatcher.Start();
        }

        ~ScreenBrightness()
        {
            _handler.Clear();
            UnregisterWmiBrightnessChangedEvent();
        }

        private void WmiEventHandler(object sender, EventArrivedEventArgs e)
        {
            var args = BrightnessChangedEventArgs.From(e);

            _handler.ForEach(h => h(this, args));
        }
    }
}