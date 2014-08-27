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
    /// <remarks>This class implements IDisposebale and should be disposed before application exists.</remarks>
    public class Display : IDisposable
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
        
        private byte _currentBrightness;

        private Display()
        {
            ScreenBrightnessWmiEventHandler.Instance.Register(this);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public void Dispose()
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
        /// Indicates whether driver and monitor supports setting and getting display brightness.
        /// </summary>
        public bool IsBrightnessSupported { get; internal set; }

        /// <summary>
        /// Video input type.
        /// </summary>
        public VideoInputType VideoInputType { get; private set; }

        public int LevelCount { get; private set; }

        /// <summary>
        /// Current brightness as a percentage of total brightness.
        /// </summary>
        /// <remarks>If IsBrightnessSupported is true: Get/sets current screen brightness. If IsBrightnessSupported is false: Get return 0, set throws a NotSupportedException.</remarks>
        public byte CurrentBrightness
        {
            get { return IsBrightnessSupported ? _currentBrightness : (byte)0; }
            set
            {
                if(!IsBrightnessSupported)
                    throw new NotSupportedException("This feature is not supported for this display.");
                if (value == _currentBrightness) return;

                _currentBrightness = value;

                CurrentBrightnessChanged.CheckedInvoke(this);
                SetWmiBrightness(value);
            }
        }

        public void SetWmiBrightness(byte value, int timeOut = 0)
        {
            try
            {
                var managementObject = 
                    new ManagementObject("root\\WMI", 
                    "WmiMonitorBrightnessMethods.InstanceName='{0}'".FormatWith(InstanceName),
                    null);

                // Obtain in-parameters for the method
                var inParams = managementObject.GetMethodParameters("WmiSetBrightness");

                // Add the input parameters.
                inParams["Brightness"] = value;
                inParams["Timeout"] = timeOut;
                // Execute the method and obtain the return values.
                managementObject.InvokeMethod("WmiSetBrightness", inParams, null);
            }
            catch
            {
            }
        }

        private static IList<Display> _knownDisplays = new List<Display>();

        public static IList<Display> AvailableDisplays
        {
            get
            {
                try
                {
                    var searcher = new ManagementObjectSearcher("root\\WMI", "SELECT * FROM WmiMonitorBasicDisplayParams");

                    var displays = new List<Display>(4);
                    foreach (ManagementObject managementObject in searcher.Get())
                    {
                        var instaneName = managementObject["InstanceName"].ToString();
                        var display = _knownDisplays.FirstOrDefault(d => d.InstanceName == instaneName);
                        if (display != null)
                        {
                            UpdateDisplay(display, managementObject);
                            displays.Add(display);
                        }
                        else
                        {
                            displays.Add(CreateNewDisplay(managementObject));
                        }
                    }
                    var nowLongerAvailableDisplays = _knownDisplays.Except(displays);
                    nowLongerAvailableDisplays.ForEach(d => d.Dispose());
                    _knownDisplays = displays;
                    UpdateBrightnessAndLevelCount();
                }
                catch (ManagementException)
                {
                }

                return _knownDisplays;
            }
        }

        private static void UpdateDisplay(Display display, ManagementObject managementObject)
        {
            display.Active = bool.Parse(managementObject["Active"].ToString());
            display.DisplayTransferCharacteristic =
                byte.Parse(managementObject["DisplayTransferCharacteristic"].ToString());
            display.MaxHorizontalImageSize = byte.Parse(managementObject["MaxHorizontalImageSize"].ToString());
            display.MaxVerticalImageSize = byte.Parse(managementObject["MaxVerticalImageSize"].ToString());
            display.VideoInputType = (VideoInputType) byte.Parse(managementObject["VideoInputType"].ToString());
            SupportedDisplayFeaturesDescriptor.TryCreate(
                managementObject["SupportedDisplayFeatures"] as ManagementBaseObject,
                out display.SupportedDisplayFeatures);
        }

        private static Display CreateNewDisplay(ManagementBaseObject managementObject)
        {
            var display = new Display
            {
                Active = bool.Parse(managementObject["Active"].ToString()),
                DisplayTransferCharacteristic =
                    byte.Parse(managementObject["DisplayTransferCharacteristic"].ToString()),
                InstanceName = managementObject["InstanceName"].ToString(),
                MaxHorizontalImageSize =
                    byte.Parse(managementObject["MaxHorizontalImageSize"].ToString()),
                MaxVerticalImageSize = byte.Parse(managementObject["MaxVerticalImageSize"].ToString()),
                VideoInputType =
                    (VideoInputType)byte.Parse(managementObject["VideoInputType"].ToString())
            };
            SupportedDisplayFeaturesDescriptor.TryCreate(
                managementObject["SupportedDisplayFeatures"] as ManagementBaseObject,
                out display.SupportedDisplayFeatures);

            return display;
        }

        private static void UpdateBrightnessAndLevelCount()
        {
            var searcher = new ManagementObjectSearcher("root\\WMI", "SELECT * FROM WmiMonitorBrightness");
            foreach (ManagementObject queryObj in searcher.Get())
            {
                var instanceName = queryObj.Properties["InstanceName"].Value.ToString();
                var current = byte.Parse(queryObj.Properties["CurrentBrightness"].Value.ToString());
                var level = byte.Parse(queryObj.Properties["Levels"].Value.ToString());
                var display = _knownDisplays.FirstOrDefault(d => d.InstanceName == instanceName);
                if (display == null) continue;

                display.IsBrightnessSupported = true;
                display.CurrentBrightness = current;
                display.LevelCount = level;
            }
        }
    }
}