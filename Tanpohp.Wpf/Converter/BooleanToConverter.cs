#region usings

using System;
using System.Globalization;
using System.Windows.Data;

#endregion

namespace Tanpohp.Wpf.Converter
{
    /// <summary>
    /// Converts a boolean to T.
    /// </summary>
    /// <typeparam name="T">Generic parameter.</typeparam>
    public abstract class BooleanToConverter<T> : IValueConverter
    {
        public T TrueValue { get; set; }

        public T FalseValue { get; set; }

        public T ErrorValue { get; set; }

        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is bool)) return ErrorValue;

            return (bool) value ? TrueValue : FalseValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}