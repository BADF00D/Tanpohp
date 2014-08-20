using Tanpohp.Annotations.Resharper;

namespace Tanpohp.Extensions
{
    public static class StringExtension
    {
        /// <summary>
        /// Simply wrapps string.Format method.
        /// </summary>
        /// <param name="format">String with format.</param>
        /// <param name="parameter">Arrya with parameter.</param>
        /// <returns></returns>
        [StringFormatMethod("format")]
        public static string FormatWith(this string format, params object[] parameter)
        {
            return string.Format(format, parameter);
        }
    }
}
