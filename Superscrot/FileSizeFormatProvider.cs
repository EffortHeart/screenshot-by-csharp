using System;

namespace Superscrot
{
    /// <summary>
    /// Defines a method that supports formatting the value of an object as a
    /// file size.
    /// </summary>
    public class FileSizeFormatProvider : IFormatProvider, ICustomFormatter
    {
        /// <summary>
        /// Converts the value of a specified object to an equivalent string
        /// representation using specified format and culture-specific
        /// formatting information
        /// </summary>
        /// <param name="format">A format string containing formatting specifications.</param>
        /// <param name="arg">An object to format.</param>
        /// <param name="formatProvider">
        /// An object that supplies format information about the current instance.
        /// </param>
        /// <returns>
        /// The string representation of the value of <paramref name="arg"/>,
        /// formatted as specified by <paramref name="format"/> and <paramref name="formatProvider"/>.
        /// </returns>
        public string Format(string format, object arg, IFormatProvider formatProvider)
        {
            if (arg is long)
            {
                var value = (long)arg;
                return PathUtility.FormatFileSize(value);
            }

            // Fallback
            if (arg is IFormattable)
            {
                var value = (IFormattable)arg;
                return value.ToString(format, formatProvider);
            }

            return arg.ToString();
        }

        /// <summary>
        /// Returns an object that provides formatting services for the
        /// specified type.
        /// </summary>
        /// <param name="formatType">
        /// An object that specifies the type of format object to return.
        /// </param>
        /// <returns>
        /// An instance of the object specified by <paramref
        /// name="formatType"/>, if the <see cref="IFormatProvider"/>
        /// implementation can supply that type of object; otherwise, <c>null</c>.
        /// </returns>
        public object GetFormat(Type formatType)
        {
            if (formatType == typeof(ICustomFormatter))
                return this;
            return null;
        }
    }
}
