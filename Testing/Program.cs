using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tanpohp.Wmi.Hardware;

namespace Testing
{
    class Program
    {
        static void Main(string[] args)
        {
            ScreenBrightness.BrightnessChangedEvent += OnBrightnessChanged;
            Console.ReadLine();
            ScreenBrightness.BrightnessChangedEvent -= OnBrightnessChanged;
        }

        private static void OnBrightnessChanged(object sender, BrightnessChangedEventArgs e)
        {
            Console.WriteLine(e);
        }
    }
}
