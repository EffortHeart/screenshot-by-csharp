using System;

namespace Superscrot
{
    /// <summary>
    /// Provides additional functionality to the <see cref="DateTime"/> class.
    /// </summary>
    public static class DateTimeExtensions
    {
        private static readonly DateTime UnixEpoch =
            new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        /// <summary>
        /// Converts the value of the current <see cref="DateTime"/> object to a
        /// Unix timestamp.
        /// </summary>
        /// <param name="value">
        /// The <see cref="DateTime"/> object whose value to convert.
        /// </param>
        /// <returns>
        /// A value indicating the total number of elapsed seconds since Unix epoch.
        /// </returns>
        public static long ToUnixTimestamp(this DateTime value)
        {
            var diff = value.ToUniversalTime() - UnixEpoch;
            return (long)diff.TotalSeconds;
        }
    }
}
