using System;
using Tanpohp.Extensions;
using Tanpohp.Wmi.Hardware;

namespace Testing
{
    class Program
    {
        static void Main(string[] args)
        {
            var displays = Display.GetAvailableMonitors();
            displays.ForEach(d => d.CurrentBrightnessChanged+= OnBrightnessChanged);
            Console.ReadLine();
            //var sb = new ScreenBrightness();
            //Console.WriteLine(sb.Active);
            //Console.WriteLine(sb.Current);
            //Console.WriteLine(sb.InstanceName);
            //Console.WriteLine(sb.Levels.ItemsToString());
            //sb.BrightnessChangedEvent += OnBrightnessChanged;
            //Console.ReadLine();
            //sb.BrightnessChangedEvent -= OnBrightnessChanged;
        }

        private static void OnBrightnessChanged(object sender, EventArgs e)
        {
            var display = (sender as Display);
            Console.WriteLine("#################################");
            Console.WriteLine("Active: "+display.Active);
            Console.WriteLine("Brightness: "+display.CurrentBrightness);
            Console.WriteLine("InstanceName: "+display.InstanceName);
        }
    }
}
