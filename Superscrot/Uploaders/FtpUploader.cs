using System;
using System.IO;
using System.Linq;

namespace Superscrot.Uploaders
{
    /// <summary>
    /// Provides the functionality to upload and delete screenshot to and from
    /// an FTP server.
    /// </summary>
    internal class FtpUploader : Uploader
    {
        private FTP.FtpClient client;

        /// <summary>
        /// Initializes a new instance of the <see cref="FtpUploader"/> class
        /// with the specified username and password.
        /// </summary>
        /// <param name="info">An object containing the connection info.</param>
        /// <param name="timeout">
        /// The time in milliseconds to wait for a response from the server.
        /// </param>
        public FtpUploader(ConnectionInfo info, int timeout = 30000)
        {
            client = new FTP.FtpClient(info.Host, info.Port, info.UserName,
                info.Password);
            client.Timeout = timeout;
        }

        /// <summary>
        /// Removes a screenshot from the server.
        /// </summary>
        /// <param name="path">The path to the file on the server to remove.</param>
        /// <returns>True if the file was deleted, false otherwise.</returns>
        /// <exception cref="Superscrot.ConnectionFailedException">
        /// Connectioned to the server failed.
        /// </exception>
        public override bool Delete(string path)
        {
            EnsureConnection();

            return client.DeleteFile(path);
        }

        /// <summary>
        /// Uploads a screenshot to the target location on the currently
        /// configured server.
        /// </summary>
        /// <param name="stream">The file to upload.</param>
        /// <param name="target">The path on the server to upload to.</param>
        /// <returns>True if the upload succeeded, false otherwise.</returns>
        /// <exception cref="Superscrot.ConnectionFailedException">
        /// Connectioned to the server failed.
        /// </exception>
        public override bool Upload(Stream stream, ref string target)
        {
            EnsureConnection();

            if (!client.DirectoryExists(Path.GetDirectoryName(target)))
                client.CreateDirectory(Path.GetDirectoryName(target));

            return client.Upload(stream, target);
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
        protected override string FindDuplicate(string name, string directory)
        {
            var listing = client.ListDirectory(directory);
            var duplicate = listing.FirstOrDefault(x => x.Contains(name));

            if (duplicate.StartsWith("/"))
                return duplicate;
            return PathUtility.UriCombine(directory, duplicate);
        }

        private void EnsureConnection()
        {
            if (!client.AttemptConnection())
            {
                throw new ConnectionFailedException(
                    SR.ConnectionFailed.With(client.Hostname), client.Hostname);
            }
        }
    }
}
