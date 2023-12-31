using System;
using System.IO;

namespace Superscrot.Uploaders
{
    /// <summary>
    /// Represents an abstract base class for uploading and deleting files.
    /// </summary>
    public abstract class Uploader : IDisposable
    {
        /// <summary>
        /// Occurs when a duplicate file was found on the server before the
        /// screenshot was uploaded.
        /// </summary>
        public event EventHandler<DuplicateFileEventArgs> DuplicateFileFound;

        /// <summary>
        /// Returns a new instance of the <see cref="Uploader"/> class for the
        /// specified configuration.
        /// </summary>
        /// <param name="config">
        /// A <see cref="Configuration"/> object that contains the connection
        /// info and settings.
        /// </param>
        /// <returns>A new object that derives from <see cref="Uploader"/>.</returns>
        public static Uploader Create(Configuration config)
        {
            var info = new ConnectionInfo(config);

            if (config.UseSSH)
            {
                return new SftpUploader(info, config.FtpTimeout);
            }
            else
            {
                return new FtpUploader(info, config.FtpTimeout);
            }
        }

        /// <summary>
        /// Checks if the server contains a file containing <paramref
        /// name="name"/> in the name, and returns a value indicating whether to
        /// continue the upload or not.
        /// </summary>
        /// <param name="name">
        /// The name to check for in files on the server. This is typically the
        /// name of the original file being uploaded. If this is <c>null</c> or
        /// the empty string, this function will always return <c>true</c>.
        /// </param>
        /// <param name="target">The name of the file to upload.</param>
        /// <returns>
        /// <c>true</c> if the upload should continue (as <paramref
        /// name="target"/>), <c>false</c> otherwise.
        /// </returns>
        public virtual bool CheckDuplicates(string name, ref string target)
        {
            if (string.IsNullOrEmpty(name)) return true;

            var directory = Path.GetDirectoryName(target).Replace('\\', '/');
            var duplicate = FindDuplicate(name, directory);

            if (duplicate != null)
            {
                var e = new DuplicateFileEventArgs(this, target, duplicate);

                OnDuplicateFileFound(e);
                switch (e.Action)
                {
                    case DuplicateFileAction.Replace:
                        target = duplicate;
                        return true;

                    case DuplicateFileAction.Abort:
                        return false;

                    case DuplicateFileAction.Ignore:
                    default:
                        return true;
                }
            }

            return true;
        }

        /// <summary>
        /// Removes a file from the server.
        /// </summary>
        /// <param name="path">The path to the file on the server to remove.</param>
        /// <returns>True if the file was deleted, false otherwise.</returns>
        /// <exception cref="Superscrot.ConnectionFailedException">
        /// Connectioned to the server failed
        /// </exception>
        public abstract bool Delete(string path);

        /// <summary>
        /// Cleans up resources used by this instance.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Uploads a file to the target location on the currently configured server.
        /// </summary>
        /// <param name="stream">The file to upload.</param>
        /// <param name="target">The path on the server to upload to.</param>
        /// <returns>True if the upload succeeded, false otherwise.</returns>
        /// <exception cref="Superscrot.ConnectionFailedException">
        /// Connection to the server failed.
        /// </exception>
        public abstract bool Upload(Stream stream, ref string target);

        /// <summary>
        /// Uploads a file to the target location on the currently configured server.
        /// </summary>
        /// <param name="path">The path to the file to upload.</param>
        /// <param name="target">The path on the server to upload to.</param>
        /// <returns>True if the upload succeeded, false otherwise.</returns>
        /// <exception cref="Superscrot.ConnectionFailedException">
        /// Connection to the server failed.
        /// </exception>
        public virtual bool Upload(string path, ref string target)
        {
            using (var stream = File.OpenRead(path))
            {
                return Upload(stream, ref target);
            }
        }

        /// <summary>
        /// Cleans up resources used by this instance.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release managed resources.</param>
        protected virtual void Dispose(bool disposing)
        {
        }

        /// <summary>
        /// Finds files partially matching the specified name in a directory on
        /// the server.
        /// </summary>
        /// <param name="name">The name that should be searched for.</param>
        /// <param name="directory">
        /// The directory on the server to search in.
        /// </param>
        /// <returns>
        /// The full name of the first matching file on the server, or
        /// <c>null</c> if no matching files could be found.
        /// </returns>
        protected abstract string FindDuplicate(string name, string directory);

        /// <summary>
        /// Raises the <see cref="DuplicateFileFound"/> event.
        /// </summary>
        /// <param name="e">
        /// A <see cref="DuplicateFileEventArgs"/> object that contains the
        /// event data.
        /// </param>
        protected virtual void OnDuplicateFileFound(DuplicateFileEventArgs e)
        {
            var duplicateFileFound = DuplicateFileFound;
            if (duplicateFileFound != null)
            {
                duplicateFileFound(this, e);
            }
        }
    }
}
