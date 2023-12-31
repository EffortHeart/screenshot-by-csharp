using System;
using System.Collections.Generic;

namespace FTP
{
    /// <summary>
    /// Provides methods commonly used by all classes.
    /// </summary>
    public static class Common
    {
        /// <summary>
        /// Returns a copy of this array without empty elements.
        /// </summary>
        /// <param name="input">asdfasdfasdf</param>
        public static string[] Trim(this string[] input)
        {
            List<string> output = new List<string>();
            foreach (string item in input)
            {
                if (item != null && item.Length > 0)
                {
                    output.Add(item);
                }
            }

            return output.ToArray();
        }
    }
}
