using System;
using System.Linq;
using Tanpohp.Extensions;
using Tanpohp.Wmi.Hardware;

namespace Testing
{
    class Program
    {
        static void Main(string[] args)
        {
            var displays = Display.GetAvailableDisplays();
            displays.ForEach(d => d.CurrentBrightnessChanged+= OnBrightnessChanged);
            displays.Where(d =>d.IsBrightnessSupported).ForEach(d => 
                d.CurrentBrightness = 50);
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
