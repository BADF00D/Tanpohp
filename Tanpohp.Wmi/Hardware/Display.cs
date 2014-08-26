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
    /// The WmiMonitorBasicDisplayParams WMI class represents the basic display features of a computer monitor. 
    /// The value of the VideoInputType property indicates whether the monitor is analog or digital. 
    /// Data in this class corresponds to data in the Basic Display Parameters/Features block of 
    /// Video Electronics Standard Association (VESA) Enhanced Extended Display Identification 
    /// Data (E-EDID) standard.
    /// </summary>
    /// <remarks>MaxHorizontalImageSize and MaxVerticalImageSize represent the maximum image dimensions that 
    /// the monitor can correctly display for the entire set of supported timing and format combinations. 
    /// The maximum image dimension is defined by VESA Video Image Area Definition (VIAD) Standard and is 
    /// rounded to the nearest centimeter. The host computer system can use this data to select the image 
    /// size and aspect ratio that will allow properly scaled text. Be aware that, if either or both of these 
    /// fields are zero, then the system makes no assumptions about the display size. For example, the size of 
    /// a projection display may be undetermined.
    /// </remarks>
    public class Display
    {
        /// <summary>
        /// Occures when Active changed.
        /// </summary>
        public EventHandler ActiveChanged;

        /// <summary>
        /// Occures when brightness is changed.
        /// </summary>
        public EventHandler CurrentBrightnessChanged;

        /// <summary>
        /// Display features supported by the monitor.
        /// </summary>
        public SupportedDisplayFeaturesDescriptor SupportedDisplayFeatures;

        private bool _active;
        private byte? _currentBrightness;

        private Display()
        {
            ScreenBrightnessWmiEventHandler.Instance.Register(this);
        }

        ~Display()
        {
            ScreenBrightnessWmiEventHandler.Instance.Unregister(this);
        }

        /// <summary>
        /// Maximum vertical image size in centimeters. For more information, see Remarks.
        /// </summary>
        public byte MaxVerticalImageSize { get; private set; }

        /// <summary>
        /// Maximum horizontal image size in centimeters. For more information, see Remarks.
        /// </summary>
        public byte MaxHorizontalImageSize { get; private set; }

        /// <summary>
        /// Indicates the active monitor.
        /// </summary>
        public bool Active
        {
            get { return _active; }
            internal set
            {
                if (value == _active) return;

                _active = value;

                ActiveChanged.CheckedInvoke(this);
            }
        }

        /// <summary>
        /// Display transfer characteristic (100*Gamma-100).
        /// </summary>
        public byte DisplayTransferCharacteristic { get; private set; }

        /// <summary>
        /// Name of the specific monitor instance.
        /// </summary>
        public string InstanceName { get; private set; }

        /// <summary>
        /// Video input type.
        /// </summary>
        public VideoInputType VideoInputType { get; private set; }

        public int LevelCount { get; private set; }

        /// <summary>
        /// Current brightness as a percentage of total brightness.
        /// </summary>
        /// <remarks>Null if this monitor does not report this feature.</remarks>
        public byte? CurrentBrightness
        {
            get { return _currentBrightness; }
            set
            {
                if (value == _currentBrightness) return;

                _currentBrightness = value;

                CurrentBrightnessChanged.CheckedInvoke(this);
            }
        }

        public static IList<Display> GetAvailableMonitors()
        {
            var monitors = new List<Display>(4);
            try
            {
                var searcher = new ManagementObjectSearcher("root\\WMI", "SELECT * FROM WmiMonitorBasicDisplayParams");

                foreach (ManagementObject queryObj in searcher.Get())
                {
                    var monitor = new Display
                                      {
                                          Active = bool.Parse(queryObj["Active"].ToString()),
                                          DisplayTransferCharacteristic =
                                              byte.Parse(queryObj["DisplayTransferCharacteristic"].ToString()),
                                          InstanceName = queryObj["InstanceName"].ToString(),
                                          MaxHorizontalImageSize =
                                              byte.Parse(queryObj["MaxHorizontalImageSize"].ToString()),
                                          MaxVerticalImageSize = byte.Parse(queryObj["MaxVerticalImageSize"].ToString()),
                                          VideoInputType =
                                              (VideoInputType) byte.Parse(queryObj["VideoInputType"].ToString())
                                      };
                    SupportedDisplayFeaturesDescriptor.TryCreate(
                        queryObj["SupportedDisplayFeatures"] as ManagementBaseObject,
                        out monitor.SupportedDisplayFeatures);

                    monitors.Add(monitor);
                }
                UpdateBrightnessAndLevelCount(monitors);
            }
            catch (ManagementException)
            {
            }

            return monitors;
        }

        private static void UpdateBrightnessAndLevelCount(IEnumerable<Display> monitors)
        {
            var searcher = new ManagementObjectSearcher("root\\WMI", "SELECT * FROM WmiMonitorBrightness");
            foreach (ManagementObject queryObj in searcher.Get())
            {
                var instanceName = queryObj.Properties["InstanceName"].Value.ToString();
                var current = byte.Parse(queryObj.Properties["CurrentBrightness"].Value.ToString());
                var level = byte.Parse(queryObj.Properties["Levels"].Value.ToString());
                var display = monitors.FirstOrDefault(d => d.InstanceName == instanceName);
                if (display == null) continue;

                display.CurrentBrightness = current;
                display.LevelCount = level;
            }
        }
    }
}