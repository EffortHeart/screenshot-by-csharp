using System;
using System.Drawing;

namespace Superscrot
{
    /// <summary>
    /// Provides methods to workaround XML serialization problems with the <see
    /// cref="System.Drawing.Color"/> class.
    /// </summary>
    public static class XmlColor
    {
        /// <summary>
        /// Represents the different kinds of representations of a color.
        /// </summary>
        public enum ColorFormat
        {
            /// <summary>
            /// Indicates a named color.
            /// </summary>
            NamedColor,

            /// <summary>
            /// Indicates a color with alpha, red, green and blue components.
            /// </summary>
            ARGBColor
        }

        /// <summary>
        /// Converts a string serialized with <see cref="SerializeColor"/> to a
        /// new instance of the <see cref="System.Drawing.Color"/> class.
        /// </summary>
        /// <param name="color">
        /// A string value containing the color serialized with <see cref="SerializeColor"/>.
        /// </param>
        /// <returns>
        /// A new instance of the <see cref="System.Drawing.Color"/> class.
        /// </returns>
        public static Color DeserializeColor(string color)
        {
            try
            {
                byte a, r, g, b;
                string[] pieces = color.Split(new char[] { ':' });
                ColorFormat colorType = (ColorFormat)Enum.Parse(typeof(ColorFormat), pieces[0], true);

                switch (colorType)
                {
                    case ColorFormat.NamedColor:
                        return Color.FromName(pieces[1]);

                    case ColorFormat.ARGBColor:
                        a = byte.Parse(pieces[1]);
                        r = byte.Parse(pieces[2]);
                        g = byte.Parse(pieces[3]);
                        b = byte.Parse(pieces[4]);
                        return Color.FromArgb(a, r, g, b);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex);
            }
            return Color.Empty;
        }

        /// <summary>
        /// Returns a string that represents the specified <see cref="System.Drawing.Color"/>.
        /// </summary>
        /// <param name="color">
        /// The <see cref="System.Drawing.Color"/> to serialize.
        /// </param>
        /// <returns>
        /// A string representing the color that can be deserialized using the
        /// <see cref="DeserializeColor"/> method.
        /// </returns>
        public static string SerializeColor(Color color)
        {
            try
            {
                if (color.IsNamedColor)
                {
                    return string.Format("{0}:{1}", ColorFormat.NamedColor, color.Name);
                }
                else
                {
                    return string.Format("{0}:{1}:{2}:{3}:{4}", ColorFormat.ARGBColor, color.A, color.R, color.G, color.B);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex);
                return "NamedColor:Transparent";
            }
        }
    }
}
