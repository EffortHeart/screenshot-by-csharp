using System;

namespace Superscrot.Uploaders
{
    /// <summary>
    /// Represents the possible actions that can be taken when a duplicate file
    /// was found.
    /// </summary>
    public enum DuplicateFileAction
    {
        /// <summary>
        /// Indicates that the file should uploaded as-is, ignoring the existing
        /// file on the server.
        /// </summary>
        Ignore,

        /// <summary>
        /// Indicates that the file on the server should be replaced with the
        /// file being uploaded.
        /// </summary>
        Replace,

        /// <summary>
        /// Indicates that the upload should be aborted.
        /// </summary>
        Abort
    }

    /// <summary>
    /// Provides data for the <see cref="Uploader.DuplicateFileFound"/> event.
    /// </summary>
    public class DuplicateFileEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see
        /// cref="DuplicateFileEventArgs"/> class with the uploader being used,
        /// the original destination and the name of the duplicate file.
        /// </summary>
        /// <param name="uploader">
        /// The <see cref="Uploader"/> instance being used.
        /// </param>
        /// <param name="target">The original upload destination.</param>
        /// <param name="duplicate">
        /// The name of a possible duplicate file on the server.
        /// </param>
        public DuplicateFileEventArgs(Uploader uploader, string target,
            string duplicate)
        {
            Uploader = uploader;
            TargetFileName = target;
            DuplicateFileName = duplicate;
            Action = DuplicateFileAction.Ignore;
        }

        /// <summary>
        /// Gets or sets the action to be taken.
        /// </summary>
        public DuplicateFileAction Action { get; set; }

        /// <summary>
        /// Gets the name of the file on the server that matches the name of the
        /// file being uploaded.
        /// </summary>
        public string DuplicateFileName { get; }

        /// <summary>
        /// Gets name of the file being uploaded.
        /// </summary>
        public string TargetFileName { get; }

        /// <summary>
        /// Gets the <see cref="Screenshot"/> that caused the event to trigger.
        /// </summary>
        public Uploader Uploader { get; }
    }
}
