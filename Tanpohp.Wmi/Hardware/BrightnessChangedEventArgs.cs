using System;
using System.Management;
using Tanpohp.Annotations.Resharper;
using Tanpohp.Extensions;

namespace Tanpohp.Wmi.Hardware
{
    public class BrightnessChangedEventArgs : EventArgs
    {
        public BrightnessChangedEventArgs([NotNull]string instanceName, bool active, byte brightness)
        {
            InstanceName = instanceName;
            Active = active;
            Brightness = brightness;
        }

        public byte Brightness { get; private set; }

        public bool Active { get; private set; }

        public string InstanceName { get; private set; }

        public override string ToString()
        {
            return "{0}(Active:{1}/Brightness:{2})".FormatWith(InstanceName, Active, Brightness);
        }

        public static BrightnessChangedEventArgs From(EventArrivedEventArgs args)
        {
            var instanceName = args.NewEvent.Properties["InstanceName"].Value.ToString();
            var active = bool.Parse(args.NewEvent.Properties["Active"].Value.ToString());
            var brightness = byte.Parse(args.NewEvent.Properties["Brightness"].Value.ToString());

            return new BrightnessChangedEventArgs(instanceName, active, brightness);
        }
    }
}