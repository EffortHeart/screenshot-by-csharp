using System;

namespace Superscrot
{
    /// <summary>
    /// Provides data for an upload event that can be cancelled.
    /// </summary>
    public class UploadingEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UploadingEventArgs"/>
        /// class with the specified file name.
        /// </summary>
        /// <param name="fileName">The name of the file being uploaded.</param>
        public UploadingEventArgs(string fileName)
        {
            FileName = fileName;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the upload should be cancelled.
        /// </summary>
        public bool Cancel { get; set; }

        /// <summary>
        /// Gets or sets the name of the file that the screenshot will be
        /// uploaded as.
        /// </summary>
        public string FileName { get; set; }
    }
}
