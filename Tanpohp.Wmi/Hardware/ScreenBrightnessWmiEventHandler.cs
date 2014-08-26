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
    internal class ScreenBrightnessWmiEventHandler
    {
        private const string ManagementScopeName = "root\\WMI";

        private const string BrightnessEventQuery = "Select * From WmiMonitorBrightnessEvent";

        private const string ActiveKey = "Active";

        private const string BrightnessKey = "Brightness";

        private const string InstanceNameKey = "InstanceName";

        private static ScreenBrightnessWmiEventHandler _instance;

        private static readonly object LockKey = new object();
        private readonly IList<Display> _supervisedDisplays = new List<Display>(4);


        private ManagementEventWatcher _eventWatcher;

        private ScreenBrightnessWmiEventHandler()
        {
            RegisterForWmiBrightnessChangedEvent();
        }

        public static ScreenBrightnessWmiEventHandler Instance
        {
            get
            {
                if (_instance != null) return _instance;
                lock (LockKey)
                {
                    if (_instance == null) _instance = new ScreenBrightnessWmiEventHandler();
                }
                return _instance;
            }
        }

        /// <summary>
        /// Registers the given display, it it is not registered already.
        /// </summary>
        /// <param name="display"></param>
        public void Register(Display display)
        {
            lock (LockKey)
            {
                if (_supervisedDisplays.Contains(display)) return;
                _supervisedDisplays.Add(display);
            }
        }

        /// <summary>
        /// Unregisters the display if it is registered.
        /// </summary>
        /// <param name="display"></param>
        public void Unregister(Display display)
        {
            lock (LockKey)
            {
                if (!_supervisedDisplays.Contains(display)) return;
                _supervisedDisplays.Remove(display);
            }
        }

        private void RegisterForWmiBrightnessChangedEvent()
        {
            try
            {
                var scope = new ManagementScope(ManagementScopeName);
                scope.Connect();

                _eventWatcher = new ManagementEventWatcher(scope, new EventQuery(BrightnessEventQuery));
                _eventWatcher.EventArrived += WmiEventHandler;
                _eventWatcher.Start();
            }
            catch (Exception e)
            {
                if (_eventWatcher != null)
                {
                    _eventWatcher.EventArrived -= WmiEventHandler;
                    _eventWatcher.Dispose();
                    _eventWatcher = null;
                }
                throw new Exception(
                    "Unable to register for ScreenBrightnessChangedEvent. See inner exception for details.", e);
            }
        }

        ~ScreenBrightnessWmiEventHandler()
        {
            UnregisterWmiBrightnessChangedEvent();
        }

        private void UnregisterWmiBrightnessChangedEvent()
        {
            if (_eventWatcher == null) return;

            _eventWatcher.Stop();
            _eventWatcher.EventArrived -= WmiEventHandler;
            _eventWatcher.Dispose();
        }

        private void WmiEventHandler(object sender, EventArrivedEventArgs e)
        {
            lock (LockKey)
            {
                if (_supervisedDisplays.IsEmpty()) return;

                var instanceName = e.NewEvent[InstanceNameKey].ToString();
                var display = _supervisedDisplays.FirstOrDefault(d => d.InstanceName == instanceName);
                if (display == null) return;

                var active = bool.Parse(e.NewEvent[ActiveKey].ToString());
                var brightness = byte.Parse(e.NewEvent[BrightnessKey].ToString());

                display.Active = active;
                display.CurrentBrightness = brightness;
            }
        }
    }
}