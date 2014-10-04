using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tanpohp.Extensions
{
    public static class DateTimeExtension
    {
        /// <summary>
        /// Determines whether date is in future from parameter pointOfView.
        /// </summary>
        /// <param name="date">DateTime that has to be checked.</param>
        /// <param name="pointOfView">Point of view used to determine if date is in future.</param>
        /// <returns></returns>
        public static bool IsFuture(this DateTime date, DateTime pointOfView)
        {
            return date.Date > pointOfView.Date;
        }

        /// <summary>
        /// Determines whether date is in future from now.
        /// </summary>
        /// <param name="date">DateTime that has to be checked.</param>
        public static bool IsFuture(this DateTime date)
        {
            return date.IsFuture(DateTime.Now);
        }

        /// <summary>
        /// Determines whether date is in past from parameter pointOfView.
        /// </summary>
        /// <param name="date">DateTime that has to be checked.</param>
        /// <param name="pointOfView">Point of view used to determine if date is in future.</param>
        public static bool IsPast(this DateTime date, DateTime pointOfView)
        {
            return date.Date < pointOfView.Date;
        }

        /// <summary>
        /// Determines whether date is in past from now.
        /// </summary>
        /// <param name="date">DateTime that has to be checked.</param>
        public static bool IsPast(this DateTime date)
        {
            return date.IsPast(DateTime.Now);
        }
    }
}
