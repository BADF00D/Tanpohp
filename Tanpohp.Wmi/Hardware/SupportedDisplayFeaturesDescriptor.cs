#region usings

using System.Management;

#endregion

namespace Tanpohp.Wmi.Hardware
{
    /// <summary>
    /// The SupportedDisplayFeaturesDescriptorrepresents the supported display features of the monitor. 
    /// The information in this class corresponds to data in the Video Input Definition of the Video 
    /// Electronics Standard Association (VESA) Enhanced Extended Display Identification Data (E-EDID) standard.
    /// </summary>
    public class SupportedDisplayFeaturesDescriptor
    {
        /// <summary>
        /// Support for active off and very low power. The display consumes less power when it receives a 
        /// timing signal that is outside the declared active operating range. The display will revert to 
        /// normal operation if the timing signal returns to the normal operating range. Examples of timing 
        /// signals outside the normal operating range are no sync signals or no DE signal.
        /// </summary>
        public bool ActiveOffSupported { get; private set; }

        /// <summary>
        /// Type of display for the monitor. The following table lists possible values.
        /// </summary>
        public DisplayType DisplayType { get; private set; }

        /// <summary>
        /// Indicates whether the display has GTF support. If True, the display supports timings based 
        /// on the GTF standard using default GTF parameter values.
        /// </summary>
        public bool GtfSupported { get; private set; }

        /// <summary>
        /// Indicates whether the display has has a preferred timing mode. If True, the first detailed 
        /// timing block contains the preferred timing mode of the monitor. Use of preferred timing mode 
        /// is required by EDID v.1.3 and higher.
        /// </summary>
        public bool HasPreferredTimingMode { get; private set; }

        /// <summary>
        /// If True, the display supports sRGB.
        /// </summary>
        public bool SRgbSupported { get; private set; }

        /// <summary>
        /// Indicates whether the display supports VESA Display Power Management Signaling (DPMS) standby. 
        /// If True, DPMS standby is supported.
        /// </summary>
        public bool StandbySupported { get; private set; }

        /// <summary>
        /// Indicates whether the display supports VESA Display Power Management Signaling (DPMS) suspend. 
        /// If True, DPMS suspend is supported.
        /// </summary>
        public bool SuspendSupported { get; private set; }

        public static bool TryCreate(ManagementBaseObject managementObject,
                                     out SupportedDisplayFeaturesDescriptor descriptor)
        {
            descriptor = new SupportedDisplayFeaturesDescriptor
                             {
                                 ActiveOffSupported = (bool) managementObject["ActiveOffSupported"],
                                 GtfSupported = (bool) managementObject["GtfSupported"],
                                 HasPreferredTimingMode = (bool) managementObject["HasPreferredTimingMode"],
                                 SRgbSupported = (bool) managementObject["SRgbSupported"],
                                 StandbySupported = (bool) managementObject["StandbySupported"],
                                 SuspendSupported = (bool) managementObject["SuspendSupported"]
                             };
            var displayTypeRaw = byte.Parse(managementObject["DisplayType"].ToString());

            descriptor.DisplayType = (DisplayType) displayTypeRaw;

            return true;
        }
    }
}