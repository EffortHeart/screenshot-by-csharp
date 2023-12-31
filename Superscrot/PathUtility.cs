using System;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;

namespace Superscrot
{
    /// <summary>
    /// Provides methods for interacting with paths and file names.
    /// </summary>
    public static class PathUtility
    {
        private const char PlaceholderEnd = '}';
        private const char PlaceholderEscape = '\\';
        private const char PlaceholderStart = '{';

        /// <summary>
        /// Replaces one or more placeholders in a string with the specified values.
        /// </summary>
        /// <param name="format">The composite format string.</param>
        /// <param name="args">
        /// A string dictionary containing the placeholder names and values that
        /// they are to be replaced with.
        /// </param>
        /// <returns>
        /// A new string with the placeholders replaced by their values.
        /// </returns>
        public static string Format(string format, StringDictionary args)
        {
            return Format(format, args, DateTime.Now);
        }

        /// <summary>
        /// Replaces one or more placeholders in a string with the specified values.
        /// </summary>
        /// <param name="format">The composite format string.</param>
        /// <param name="args">
        /// A string dictionary containing the placeholder names and values that
        /// they are to be replaced with.
        /// </param>
        /// <param name="date">
        /// A <see cref="DateTime"/> object to format dates and times with.
        /// </param>
        /// <returns>
        /// A new string with the placeholders replaced by their values.
        /// </returns>
        public static string Format(string format, StringDictionary args,
            DateTime date)
        {
            if (format == null) return null;
            if (args == null) return format;

            var stringBuilder = new StringBuilder(format.Length);
            var i = 0;
            var c = '\0';
            while (i < format.Length)
            {
                c = format[i];
                i++;

                if (c == PlaceholderStart)
                {
                    var end = format.IndexOf(PlaceholderEnd, i);
                    if (end <= i)
                    {
                        var message = SR.ExpectedAtPos.With(PlaceholderEnd, i);
                        throw new FormatException(message);
                    }

                    var placeholder = format.Substring(i, end - i);
                    var value = args[placeholder];
                    if (!args.ContainsKey(placeholder))
                    {
                        try
                        {
                            value = date.ToString(placeholder);
                        }
                        catch (Exception ex)
                        {
                            var message = SR.InvalidPlaceholder.With(placeholder);
                            throw new FormatException(message, ex);
                        }
                    }

                    stringBuilder.Append(RemoveInvalidFilenameChars(value));
                    i = end + 1;
                }
                else if (c == PlaceholderEscape)
                {
                    if (i >= format.Length)
                    {
                        var message = SR.EndOfString.With(PlaceholderEscape);
                        throw new FormatException(message);
                    }

                    stringBuilder.Append(format[i]);
                    i++;
                }
                else
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Converts a numeric value into a string that represents the number
        /// expressed as a size value in bytes, kilobytes, megabytes, or
        /// gigabytes, depending on the size.
        /// </summary>
        /// <param name="size">The numeric value to be converted.</param>
        /// <returns>The converted string.</returns>
        public static string FormatFileSize(long size)
        {
            var buffer = new StringBuilder(11);
            NativeMethods.StrFormatByteSize(size, buffer, buffer.Capacity);
            return buffer.ToString();
        }

        /// <summary>
        /// Returns the given input string stripped of invalid filename characters.
        /// </summary>
        /// <param name="input">The string to format.</param>
        public static string RemoveInvalidFilenameChars(string input)
        {
            if (input == null) return string.Empty;

            var invalidChars = Path.GetInvalidFileNameChars();
            var stringBuilder = new StringBuilder();

            foreach (var c in input)
            {
                if (!invalidChars.Contains(c))
                    stringBuilder.Append(c);
            }

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Translates a path on the FTP server to an HTTP link.
        /// </summary>
        /// <param name="serverPath">The full path to the file on the server.</param>
        /// <returns>A string that contains the HTTP address of the file.</returns>
        public static string TranslateServerPath(string serverPath)
        {
            if (serverPath == null) return null;
            if (serverPath == string.Empty) return string.Empty;

            var folder = Path.GetDirectoryName(serverPath).Replace('\\', '/') + '/'; // GetDirectoryName never ends in a trailing slash, but FtpServerPath always ends in a trailing slash
            var file = Path.GetFileName(serverPath);

            if (!folder.StartsWith(Program.Config.FtpServerPath))
                throw new Exception(SR.DebasedServerPath);

            var baseUrl = folder.Replace(Program.Config.FtpServerPath, Program.Config.HttpBaseUri);
            var url = UriCombine(baseUrl, UrlEncode(file));
            return url;
        }

        /// <summary>
        /// Combines all parts as an URI, separated by forward slashes. The
        /// resulting value does not end with a forward slash.
        /// </summary>
        /// <param name="parts">
        /// A collection of strings. Backslashes are converted to forward slashes.
        /// </param>
        public static string UriCombine(params string[] parts)
        {
            string combined = string.Empty;

            foreach (string item in parts)
            {
                if (item != string.Empty)
                {
                    combined += item.Replace('\\', '/');
                    if (!combined.EndsWith("/")) combined += '/';
                }
            }

            return combined.TrimEnd('/');
        }

        /// <summary>
        /// Encodes a URL string by replacing certains characters.
        /// </summary>
        /// <param name="str">The string to URL encode.</param>
        /// <returns>The URL encoded string, or an empty string.</returns>
        public static string UrlEncode(string str)
        {
            string encoded = string.Empty;

            string[] paths = str.Split('/', '\\');
            for (int i = 0; i < paths.Length; i++)
            {
                paths[i] = Uri.EscapeDataString(paths[i]);
            }
            encoded = string.Join("/", paths);

            return encoded;
        }
    }
}
