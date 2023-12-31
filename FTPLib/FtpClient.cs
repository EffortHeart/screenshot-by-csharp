using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace FTP
{
    /// <summary>
    /// Provides common FTP functions.
    /// </summary>
    public class FtpClient
    {
        private const int BUFFER_SIZE = 8192; //8 kB
        private Uri _baseUri = null;
        private NetworkCredential _creds = null;
        private int _timeout = 30000;

        /// <summary>
        /// Initializes a new FTP client.
        /// </summary>
        /// <param name="hostname">The hostname or IP address of the FTP server.</param>
        public FtpClient(string hostname)
            : this(hostname, 21)
        { }

        /// <summary>
        /// Initializes a new FTP client.
        /// </summary>
        /// <param name="hostname">The hostname or IP address of the FTP server.</param>
        /// <param name="port">
        /// The port that the FTP server runs on (usually 21).
        /// </param>
        public FtpClient(string hostname, int port)
        {
            UriBuilder b = new UriBuilder();
            b.Host = hostname;
            b.Port = port;
            b.Scheme = Uri.UriSchemeFtp;
            _baseUri = b.Uri;
        }

        /// <summary>
        /// Initializes a new FTP client with the specified credentials.
        /// </summary>
        /// <param name="hostname">The hostname or IP address of the FTP server.</param>
        /// <param name="username">The username of an account on the FTP server.</param>
        /// <param name="password">The password associated with the username.</param>
        public FtpClient(string hostname, string username, string password)
            : this(hostname, 21, username, password)
        { }

        /// <summary>
        /// Initializes a new FTP client with the specified credentials.
        /// </summary>
        /// <param name="hostname">The hostname or IP address of the FTP server.</param>
        /// <param name="port">
        /// The port that the FTP server runs on (usually 21).
        /// </param>
        /// <param name="username">The username of an account on the FTP server.</param>
        /// <param name="password">The password associated with the username.</param>
        public FtpClient(string hostname, int port, string username, string password)
            : this(hostname, port)
        {
            _creds = new NetworkCredential(username, password);
        }

        /// <summary>
        /// Gets the hostname of the server of the <see cref="FtpClient"/>.
        /// </summary>
        public string Hostname
        {
            get
            {
                if (_baseUri == null) return null;
                return _baseUri.Host;
            }
        }

        /// <summary>
        /// Gets/sets the time in milliseconds to wait for responses to requests.
        /// </summary>
        public int Timeout
        {
            get { return _timeout; }
            set { _timeout = value; }
        }

        /// <summary>
        /// Determines whether the specified FTP status code is positive.
        /// </summary>
        /// <param name="code">
        /// The <see cref="System.Net.FtpStatusCode"/> value to check.
        /// </param>
        /// <returns>True if the code is positive, otherwise false.</returns>
        public static bool IsPositiveStatusCode(FtpStatusCode code)
        {
            switch (code)
            {
                case FtpStatusCode.CommandOK:
                case FtpStatusCode.CommandExtraneous:
                case FtpStatusCode.DirectoryStatus:
                case FtpStatusCode.FileStatus:
                case FtpStatusCode.SystemType:
                case FtpStatusCode.ClosingControl:
                case FtpStatusCode.ClosingData:
                case FtpStatusCode.EnteringPassive:
                case FtpStatusCode.LoggedInProceed:
                case FtpStatusCode.FileActionOK:
                case FtpStatusCode.PathnameCreated:
                    return true;

                default:
                    return false;
            }
        }

        /// <summary>
        /// Appends data to the end of a file on the server.
        /// </summary>
        /// <param name="input">
        /// The input stream that contains the data to upload.
        /// </param>
        /// <param name="serverpath">
        /// The relative path to the file on the server to append to.
        /// </param>
        /// <returns>True if the operation succeeded, false otherwise.</returns>
        public bool Append(Stream input, string serverpath)
        {
            try
            {
                FtpWebRequest request = CreateRequest(serverpath);
                request.Method = WebRequestMethods.Ftp.AppendFile;
                request.UseBinary = true;
                using (BinaryReader br = new BinaryReader(input))
                {
                    using (BinaryWriter bw = new BinaryWriter(request.GetRequestStream()))
                    {
                        byte[] buffer = new byte[BUFFER_SIZE];
                        int bytesRead = br.Read(buffer, 0, buffer.Length);
                        while (bytesRead > 0)
                        {
                            bw.Write(buffer, 0, bytesRead);
                            bytesRead = br.Read(buffer, 0, buffer.Length);
                        }
                        bw.Flush();
                    }
                }

                FtpWebResponse response = request.GetResponse() as FtpWebResponse;
                Console.ForegroundColor = ConsoleColor.Magenta; Console.WriteLine(response.StatusDescription); Console.ResetColor();
                response.Close();

                return IsPositiveStatusCode(response.StatusCode);
            }
            catch (Exception ex) { HandleException(ex); }

            return false;
        }

        /// <summary>
        /// Appends a file to the end of a file on the server.
        /// </summary>
        /// <param name="serverpath">
        /// The relative path to the file on the server to append to.
        /// </param>
        /// <param name="filename">
        /// The name of the file that contains the data to append.
        /// </param>
        /// <returns>True if the operation succeeded, false otherwise.</returns>
        public bool Append(string filename, string serverpath)
        {
            try
            {
                FtpWebRequest request = CreateRequest(serverpath);
                request.Method = WebRequestMethods.Ftp.AppendFile;
                request.UseBinary = true;
                using (FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    using (BinaryWriter bw = new BinaryWriter(request.GetRequestStream()))
                    {
                        byte[] buffer = new byte[BUFFER_SIZE];
                        int bytesRead = fs.Read(buffer, 0, buffer.Length);
                        while (bytesRead > 0)
                        {
                            bw.Write(buffer, 0, bytesRead);
                            bytesRead = fs.Read(buffer, 0, buffer.Length);
                        }
                        bw.Flush();
                    }
                }

                FtpWebResponse response = request.GetResponse() as FtpWebResponse;
                Console.ForegroundColor = ConsoleColor.Magenta; Console.WriteLine(response.StatusDescription); Console.ResetColor();
                response.Close();

                return IsPositiveStatusCode(response.StatusCode);
            }
            catch (Exception ex) { HandleException(ex); }

            return false;
        }

        /// <summary>
        /// Attempts to connect to the server.
        /// </summary>
        /// <returns>True if the connection is succesfull, otherwise false.</returns>
        public bool AttemptConnection()
        {
            try
            {
                FtpWebRequest request = CreateRequest("/");
                request.Method = WebRequestMethods.Ftp.PrintWorkingDirectory;

                FtpWebResponse response = request.GetResponse() as FtpWebResponse;
                return IsPositiveStatusCode(response.StatusCode);
            }
            catch (Exception ex) { HandleException(ex); }

            return false;
        }

        /// <summary>
        /// Creates a directory on the server.
        /// </summary>
        /// <param name="path">
        /// The relative path to the folder on the server to create.
        /// </param>
        /// <returns>True if the operation succeeded, false otherwise.</returns>
        public bool CreateDirectory(string path)
        {
            FtpStatusCode code = ExecuteCommonOperation(path, WebRequestMethods.Ftp.MakeDirectory);

            //If it failed because the path doesn't exist, try to create the parent directory first
            if (code == FtpStatusCode.ActionNotTakenFileUnavailable)
            {
                string parent = Path.GetDirectoryName(path);
                try
                {
                    if (!DirectoryExists(parent) && CreateDirectory(parent))
                    {
                        code = ExecuteCommonOperation(path, WebRequestMethods.Ftp.MakeDirectory);
                    }
                }
                catch { }
            }

            return IsPositiveStatusCode(code);
        }

        /// <summary>
        /// Deletes a directory on the server.
        /// </summary>
        /// <param name="path">
        /// The relative path to the folder on the server to delete.
        /// </param>
        /// <returns>True if the operation succeeded, false otherwise.</returns>
        public bool DeleteDirectory(string path)
        {
            FtpStatusCode code = ExecuteCommonOperation(path, WebRequestMethods.Ftp.RemoveDirectory);
            return IsPositiveStatusCode(code);
        }

        /// <summary>
        /// Deletes a file on the server.
        /// </summary>
        /// <param name="path">
        /// The relative path to the file on the server to delete.
        /// </param>
        /// <returns>True if the operation succeeded, false otherwise.</returns>
        public bool DeleteFile(string path)
        {
            FtpStatusCode code = ExecuteCommonOperation(path, WebRequestMethods.Ftp.DeleteFile);
            return IsPositiveStatusCode(code);
        }

        /// <summary>
        /// Checks if a directory exists on the server.
        /// </summary>
        /// <param name="path">The relative path of the folder on the server.</param>
        /// <returns>True if the directory exists, otherwise false.</returns>
        public bool DirectoryExists(string path)
        {
            try
            {
                string parent = Path.GetDirectoryName(path);
                string pathname = Path.GetFileName(path);

                string[] items = ListDirectory(parent);
                if (items != null && items.Length > 0)
                {
                    foreach (string item in items)
                    {
                        if (item == pathname) return true;
                    }
                }
            }
            catch { }

            return false;
        }

        /// <summary>
        /// Downloads data from the server.
        /// </summary>
        /// <param name="path">
        /// The relative path to the file on the server to download.
        /// </param>
        /// <param name="destination">
        /// The stream that the downloaded data will be written to.
        /// </param>
        /// <returns>True if the operation succeeded, false otherwise.</returns>
        public bool Download(string path, Stream destination)
        {
            try
            {
                FtpWebRequest request = CreateRequest(path);
                request.Method = WebRequestMethods.Ftp.DownloadFile;
                request.UseBinary = true;

                FtpWebResponse response = request.GetResponse() as FtpWebResponse;
                Console.ForegroundColor = ConsoleColor.Magenta; Console.WriteLine(response.StatusDescription); Console.ResetColor();
                Stream responseData = response.GetResponseStream();
                if (responseData != null)
                {
                    responseData.CopyTo(destination, BUFFER_SIZE);
                }
                response.Close();

                return IsPositiveStatusCode(response.StatusCode);
            }
            catch (Exception ex) { HandleException(ex); }

            return false;
        }

        /// <summary>
        /// Downloads a file on the server and saves it to the specified file,
        /// overwriting it if it exists.
        /// </summary>
        /// <param name="serverpath">
        /// The relative path to the file on the server.
        /// </param>
        /// <param name="filename">The full path to the file to save to.</param>
        /// <returns>True if the operation succeeded, false otherwise.</returns>
        public bool DownloadToFile(string serverpath, string filename)
        {
            try
            {
                FtpWebRequest request = CreateRequest(serverpath);
                request.Method = WebRequestMethods.Ftp.DownloadFile;
                request.UseBinary = true;

                FtpWebResponse response = request.GetResponse() as FtpWebResponse;
                Console.ForegroundColor = ConsoleColor.Magenta; Console.WriteLine(response.StatusDescription); Console.ResetColor();
                using (BinaryReader br = new BinaryReader(response.GetResponseStream()))
                {
                    using (FileStream fs = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.None))
                    {
                        byte[] buffer = new byte[BUFFER_SIZE];
                        int bytesRead = br.Read(buffer, 0, buffer.Length);
                        while (bytesRead > 0)
                        {
                            fs.Write(buffer, 0, bytesRead);
                            bytesRead = br.Read(buffer, 0, buffer.Length);
                        }
                        fs.Flush();
                    }
                }
                response.Close();

                return IsPositiveStatusCode(response.StatusCode);
            }
            catch (Exception ex) { HandleException(ex); }

            return false;
        }

        /// <summary>
        /// Retrieves the size of the file in bytes.
        /// </summary>
        /// <param name="path">The relative path to the file on the server.</param>
        public long GetFileSize(string path)
        {
            string data = ExecuteCommonQuery(path, WebRequestMethods.Ftp.GetFileSize);
            return long.Parse(data);
        }

        /// <summary>
        /// Retrieves the timestamp of the file.
        /// </summary>
        /// <param name="path">The relative path to the file on the server.</param>
        public DateTime GetTimestamp(string path)
        {
            string data = ExecuteCommonQuery(path, WebRequestMethods.Ftp.GetDateTimestamp);
            return DateTime.ParseExact(data, "yyyyMMddHHmmss", System.Globalization.CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Returns a collection of strings that represent directories and files
        /// in the current specified directory on the server.
        /// </summary>
        /// <param name="directory">The directory to list contents of.</param>
        public string[] ListDirectory(string directory)
        {
            try
            {
                List<string> lst = new List<string>();

                FtpWebRequest request = CreateRequest(directory);
                request.Method = WebRequestMethods.Ftp.ListDirectory;
                request.UseBinary = false;

                FtpWebResponse response = request.GetResponse() as FtpWebResponse;
                Console.ForegroundColor = ConsoleColor.Magenta; Console.WriteLine(response.StatusDescription); Console.ResetColor();
                using (StreamReader sr = new StreamReader(response.GetResponseStream()))
                {
                    string path = sr.ReadLine();
                    while (!string.IsNullOrEmpty(path))
                    {
                        lst.Add(path);
                        path = sr.ReadLine();
                    }
                }
                response.Close();

                return lst.ToArray();
            }
            catch (Exception ex) { HandleException(ex); }

            return null;
        }

        /// <summary>
        /// Returns a collection of entries that represent directories and files
        /// in the current specified directory on the server.
        /// </summary>
        /// <param name="directory">The directory to list contents of.</param>
        public FtpListingDetail[] ListDirectoryDetails(string directory)
        {
            try
            {
                List<FtpListingDetail> lst = new List<FtpListingDetail>();

                FtpWebRequest request = CreateRequest(directory);
                request.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
                request.UseBinary = false;

                FtpWebResponse response = request.GetResponse() as FtpWebResponse;
                Console.ForegroundColor = ConsoleColor.Magenta; Console.WriteLine(response.StatusDescription); Console.ResetColor();
                using (StreamReader sr = new StreamReader(response.GetResponseStream()))
                {
                    string line = sr.ReadLine();
                    while (!string.IsNullOrEmpty(line))
                    {
                        FtpListingDetail detail = new FtpListingDetail(line);
                        lst.Add(detail);
                        line = sr.ReadLine();
                    }
                }
                response.Close();

                return lst.ToArray();
            }
            catch (Exception ex) { HandleException(ex); }

            return null;
        }

        /// <summary>
        /// Renames a file or directory on the server.
        /// </summary>
        /// <param name="path">
        /// The relative path to the file or folder on the server to rename.
        /// </param>
        /// <param name="target">
        /// The new name of the file or folder to be renamed.
        /// </param>
        /// <returns>True if the operation succeeded, false otherwise.</returns>
        public bool Rename(string path, string target)
        {
            try
            {
                FtpWebRequest request = CreateRequest(path);
                request.Method = WebRequestMethods.Ftp.Rename;
                request.RenameTo = target;

                FtpWebResponse response = request.GetResponse() as FtpWebResponse;
                Console.ForegroundColor = ConsoleColor.Magenta; Console.WriteLine(response.StatusDescription); Console.ResetColor();
                response.Close();

                return IsPositiveStatusCode(response.StatusCode);
            }
            catch (Exception ex) { HandleException(ex); }

            return false;
        }

        /// <summary>
        /// Uploads data to the server.
        /// </summary>
        /// <param name="input">
        /// The input stream that contains the data to upload.
        /// </param>
        /// <param name="serverpath">
        /// The relative path to the file on the server to upload to.
        /// </param>
        /// <returns>True if the upload succeeded, otherwise false.</returns>
        public bool Upload(Stream input, string serverpath)
        {
            try
            {
                FtpWebRequest request = CreateRequest(serverpath);
                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.UseBinary = true;
                using (BinaryReader br = new BinaryReader(input))
                {
                    using (BinaryWriter bw = new BinaryWriter(request.GetRequestStream()))
                    {
                        byte[] buffer = new byte[BUFFER_SIZE];
                        int bytesRead = br.Read(buffer, 0, buffer.Length);
                        while (bytesRead > 0)
                        {
                            bw.Write(buffer, 0, bytesRead);
                            bytesRead = br.Read(buffer, 0, buffer.Length);
                        }
                        bw.Flush();
                    }
                }

                FtpWebResponse response = request.GetResponse() as FtpWebResponse;
                Console.ForegroundColor = ConsoleColor.Magenta; Console.WriteLine(response.StatusDescription); Console.ResetColor();
                response.Close();

                return IsPositiveStatusCode(response.StatusCode);
            }
            catch (Exception ex) { HandleException(ex); }

            return false;
        }

        /// <summary>
        /// Uploads a file to the server.
        /// </summary>
        /// <param name="serverpath">
        /// The relative path to the file on the server to upload to.
        /// </param>
        /// <param name="filename">
        /// The name of the file that contains the data to upload.
        /// </param>
        /// <returns>True if the upload succeeded, otherwise false.</returns>
        public bool Upload(string filename, string serverpath)
        {
            try
            {
                FtpWebRequest request = CreateRequest(serverpath);
                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.UseBinary = true;
                using (FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    using (BinaryWriter bw = new BinaryWriter(request.GetRequestStream()))
                    {
                        byte[] buffer = new byte[BUFFER_SIZE];
                        int bytesRead = fs.Read(buffer, 0, buffer.Length);
                        while (bytesRead > 0)
                        {
                            bw.Write(buffer, 0, bytesRead);
                            bytesRead = fs.Read(buffer, 0, buffer.Length);
                        }
                        bw.Flush();
                    }
                }

                FtpWebResponse response = request.GetResponse() as FtpWebResponse;
                Console.ForegroundColor = ConsoleColor.Magenta; Console.WriteLine(response.StatusDescription); Console.ResetColor();
                response.Close();

                return IsPositiveStatusCode(response.StatusCode);
            }
            catch (Exception ex) { HandleException(ex); }

            return false;
        }

        private static FtpStatusCode HandleException(Exception ex)
        {
            if (ex is WebException)
            {
                WebException wex = (WebException)ex;
                if (wex.Response is FtpWebResponse)
                {
                    FtpWebResponse r = wex.Response as FtpWebResponse;
                    Console.ForegroundColor = ConsoleColor.Yellow; Console.WriteLine(r.StatusDescription); Console.ResetColor();
                    return r.StatusCode;
                }
            }

            Console.ForegroundColor = ConsoleColor.Yellow; Console.WriteLine(ex.Message); Console.ResetColor();
            return FtpStatusCode.Undefined;
        }

        /// <summary>
        /// Creates an FTP request for the specified directory on the server.
        /// </summary>
        /// <param name="serverpath">
        /// The path on the server to create a request for.
        /// </param>
        /// <returns>
        /// A <see cref="System.Net.FtpWebRequest"/> instance, with credentials set.
        /// </returns>
        private FtpWebRequest CreateRequest(string serverpath)
        {
            FtpWebRequest request = WebRequest.Create(new Uri(_baseUri, serverpath)) as FtpWebRequest;
            request.Credentials = _creds;
            request.Timeout = Timeout;
            request.KeepAlive = true;
            request.UsePassive = true;
            return request;
        }

        /// <summary>
        /// Executes a common FTP method on the server that requires only a path
        /// and returns the status code.
        /// </summary>
        /// <param name="path">
        /// The relative path to the file or folder on the server.
        /// </param>
        /// <param name="method">The WebRequestMethods.Ftp method to execute</param>
        private FtpStatusCode ExecuteCommonOperation(string path, string method)
        {
            try
            {
                FtpWebRequest request = CreateRequest(path);
                request.Method = method;

                FtpWebResponse response = request.GetResponse() as FtpWebResponse;
                Console.ForegroundColor = ConsoleColor.Magenta; Console.WriteLine(response.StatusDescription); Console.ResetColor();
                response.Close();

                return response.StatusCode;
            }
            catch (Exception ex) { return HandleException(ex); }
        }

        /// <summary>
        /// Executes a common FTP method on the server that requires only a path
        /// and returns the status description.
        /// </summary>
        /// <param name="path">
        /// The relative path to the file or folder on the server.
        /// </param>
        /// <param name="method">The WebRequestMethods.Ftp method to execute</param>
        private string ExecuteCommonQuery(string path, string method)
        {
            try
            {
                FtpWebRequest request = CreateRequest(path);
                request.Method = method;

                FtpWebResponse response = request.GetResponse() as FtpWebResponse;
                Console.ForegroundColor = ConsoleColor.Magenta; Console.WriteLine(response.StatusDescription); Console.ResetColor();
                response.Close();

                string data = response.StatusDescription;
                if (data.IndexOf(' ') >= 0) data = data.Substring(data.IndexOf(' '));
                data = data.Trim();
                return data;
            }
            catch (Exception ex) { HandleException(ex); }

            return null;
        }
    }
}
