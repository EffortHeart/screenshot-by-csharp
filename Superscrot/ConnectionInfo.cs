namespace Superscrot
{
    /// <summary>
    /// Represents connection and authentication info.
    /// </summary>
    internal class ConnectionInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionInfo"/> class.
        /// </summary>
        public ConnectionInfo()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionInfo"/> class
        /// using the specified <see cref="Configuration"/>.
        /// </summary>
        /// <param name="config">
        /// The <see cref="Configuration"/> to load the connection info from.
        /// </param>
        public ConnectionInfo(Configuration config)
        {
            Host = config.FtpHostname;
            Port = config.FtpPort;
            UserName = config.FtpUsername;
            Password = config.FtpPassword;
            PrivateKeyPath = config.PrivateKeyPath;
        }

        /// <summary>
        /// Gets or sets the name or IP address of the server.
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// Gets or sets the password of the user to authenticate as.
        /// </summary>
        /// <remarks>
        /// If <see cref="PrivateKeyPath"/> points to an existing private key
        /// file, the password will only be used as fallback.
        /// </remarks>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the port number of the server.
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// Gets or sets the path to the private key file to authenticate with.
        /// </summary>
        /// <remarks>
        /// If the value of this property points to a valid private key, the
        /// <see cref="Password"/> property is ignored. If the private key does
        /// not exist or if it is invalid, the <see cref="Password"/> property
        /// is used as fallback authentication.
        /// </remarks>
        public string PrivateKeyPath { get; set; }

        /// <summary>
        /// Gets or sets the name of the user to authenticate as.
        /// </summary>
        public string UserName { get; set; }
    }
}
