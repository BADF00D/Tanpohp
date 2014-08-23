#region usings

using System;
using System.Linq;
using System.Management;

#endregion

namespace Tanpohp.Wmi.Hardware
{
    /// <summary>
    /// This class is able to set screen brightness.
    /// </summary>
    public static class ScreenBrightness
    {
        private const string ManagementScopeName = "root\\WMI";

        private const string QuerySelector = "WmiMonitorBrightnessMethods";

        private const string SetBrightnessMethodName = "WmiSetBrightness";

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
    }
}
