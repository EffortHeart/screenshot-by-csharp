using System;

namespace FTP
{
    /// <summary>
    /// Holds detailed information about a file or directory.
    /// </summary>
    public struct FtpListingDetail
    {
        /// <summary>
        /// The group of the file
        /// </summary>
        public string Group;

        /// <summary>
        /// Number of hardlinks (or whatever that number is)
        /// </summary>
        public int HardLinkCount;

        /// <summary>
        /// The modification date of the file
        /// </summary>
        public DateTime ModifiedDate;

        /// <summary>
        /// The name of the file.
        /// </summary>
        public string Name;

        /// <summary>
        /// The owner of the file
        /// </summary>
        public string Owner;

        /// <summary>
        /// Unix file system permissions
        /// </summary>
        public UnixFilePermissions Permissions;

        /// <summary>
        /// The size of the file in bytes
        /// </summary>
        public long Size;

        /// <summary>
        /// Creates a new listing detail object parsed from the specified line.
        /// </summary>
        /// <param name="line">A line as returned from an FTP LIST command.</param>
        public FtpListingDetail(string line)
        {
            if (line != null)
            {
                try
                {
                    string[] fields = line.Split(null); //Split on whitespace characters
                    fields = fields.Trim(); //Remove empty elements

                    if (fields != null && fields.Length >= 9)
                    {
                        Permissions = new UnixFilePermissions(fields[0]);
                        HardLinkCount = int.Parse(fields[1]);
                        Owner = fields[2];
                        Group = fields[3];
                        Size = long.Parse(fields[4]);

                        //Parse fields as modified date.
                        string modifiedDateString = string.Empty;
                        for (int i = 5; i < 8; i++)
                        {
                            modifiedDateString += fields[i] + ' ';
                        }
                        string[] formats = { "MMM d yyyy", "MMM d HH:mm" };
                        ModifiedDate = DateTime.ParseExact(modifiedDateString, formats, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AllowWhiteSpaces);

                        //Parse remaining fields as name
                        Name = string.Empty;
                        for (int i = 8; i < fields.Length; i++)
                        {
                            Name += fields[i] + ' ';
                        }
                        Name = Name.TrimEnd();

                        return; //Cave Johnson, we're done here.
                    }
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow; Console.WriteLine(ex); Console.ResetColor();
                }
            }

            //Since we're here parsing probably failed, so just return default values and be done.
            Permissions = new UnixFilePermissions();
            HardLinkCount = 0;
            Owner = string.Empty;
            Group = string.Empty;
            Size = 0;
            ModifiedDate = DateTime.MinValue;
            Name = string.Empty;
        }

        /// <summary>
        /// Returns whether this entry is a directory.
        /// </summary>
        public bool IsDirectory
        {
            get { return (Permissions.FileType == UnixFileTypes.Directory); }
        }

        /// <summary>
        /// Returns whether this entry is a file.
        /// </summary>
        public bool IsFile
        {
            get { return (Permissions.FileType == UnixFileTypes.Regular); }
        }
    }
}
