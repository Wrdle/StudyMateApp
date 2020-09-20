using System;
using System.IO;

namespace Mobile
{
    public static class DbConfig
    {
        public const string DatabaseFilename = "StudyMate.db3";

        public const SQLite.SQLiteOpenFlags Flags =
            // Open the daatabase in read/write mode
            SQLite.SQLiteOpenFlags.ReadWrite |
            // Create the database if it doesn't exist
            SQLite.SQLiteOpenFlags.Create |
            // Enable multi-threaded database access
            SQLite.SQLiteOpenFlags.SharedCache;

        public static string DatabasePath
        {
            get
            {
                var basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                return Path.Combine(basePath, DatabaseFilename);
            }
        }
    }
}
