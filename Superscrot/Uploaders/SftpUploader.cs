using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Renci.SshNet;

namespace Superscrot.Uploaders
{
    /// <summary>
    /// Provides the functionality to upload and delete screenshot to and from
    /// an SFTP server.
    /// </summary>
    internal class SftpUploader : Uploader
    {
        private SftpClient client;

        /// <summary>
        /// Initializes a new instance of the <see cref="SftpUploader"/> class
        /// with the specified <see cref="SftpClient"/>.
        /// </summary>
        /// <param name="info">An object containing the connection info.</param>
        /// <param name="timeout">
        /// The time in milliseconds to wait for a response from the server.
        /// </param>
        public SftpUploader(ConnectionInfo info, int timeout = 30000)
        {
            try
            {
                var keyFile = new PrivateKeyFile(info.PrivateKeyPath);
                client = new SftpClient(info.Host, info.Port, info.UserName,
                    keyFile);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
            }

            if (client == null)
            {
                client = new SftpClient(info.Host, info.Port, info.UserName,
                    info.Password);
            }
            client.ConnectionInfo.Timeout = TimeSpan.FromMilliseconds(timeout);
        }

        /// <summary>
        /// Removes a file from the server.
        /// </summary>
        /// <param name="path">The path to the file on the server to remove.</param>
        /// <returns>True if the file was deleted, false otherwise.</returns>
        /// <exception cref="Superscrot.ConnectionFailedException">
        /// Connectioned to the server failed
        /// </exception>
        public override bool Delete(string path)
        {
            EnsureConnection();
            try
            {
                client.DeleteFile(path);
            }
            finally
            {
                if (client != null)
                    client.Disconnect();
            }
            return true;
        }

        /// <summary>
        /// Uploads a file to the target location on the currently configured server.
        /// </summary>
        /// <param name="stream">The file to upload.</param>
        /// <param name="target">The path to the file on the server.</param>
        /// <returns>True if the upload succeeded, false otherwise.</returns>
        /// <exception cref="Superscrot.ConnectionFailedException">
        /// Connection to the server failed.
        /// </exception>
        public override bool Upload(Stream stream, ref string target)
        {
            if (stream == null) throw new ArgumentNullException("stream");
            EnsureConnection();

            string folder = Path.GetDirectoryName(target).Replace('\\', '/');
            SftpCreateDirectoryRecursive(folder);

            try
            {
                client.UploadFile(stream, target);
                return true;
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
                return false;
            }
            finally
            {
                if (client != null)
                    client.Disconnect();
            }
        }

        /// <summary>
        /// Cleans up resources used by this instance.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release managed resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (client != null)
                {
                    client.Dispose();
                    client = null;
                }
            }
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
            EnsureConnection();

            var listing = client.ListDirectory(directory);
            var duplicate = listing.FirstOrDefault(x => x.Name.Contains(name));

            if (duplicate != null)
            {
                return duplicate.FullName;
            }

            return null;
        }

        private void EnsureConnection()
        {
            if (client != null)
            {
                client.Connect();
                if (!client.IsConnected)
                {
                    throw new ConnectionFailedException(
                        SR.ConnectionFailed.With(client.ConnectionInfo.Host),
                        client.ConnectionInfo.Host);
                }
            }
        }

        private void SftpCreateDirectoryRecursive(string path)
        {
            if (!client.Exists(path))
            {
                string parent = Path.GetDirectoryName(path).Replace('\\', '/');
                SftpCreateDirectoryRecursive(parent);
                client.CreateDirectory(path);
            }
        }
    }
}
