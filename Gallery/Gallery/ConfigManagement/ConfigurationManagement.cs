using System;
using System.Collections.Generic;
using System.Configuration;

namespace Gallery.ConfigManagement
{
    public class ConfigurationManagement
    {
        //
        //Variables for storing key data
        public string pathToPhotosKey { get; } = "PathToPhotos";
        public string fileExtensionsKey { get; } = "FileExtensions";
        //
        //Default constants
        private const string defaultValuePathToPhotos = "/Content/Images/";
        private const string defaultValueFileExtensions = "image/jpeg;image/png";

        public string СheckValuePathToPhotos()//Adding a default value PathToPhotos
        {
            var appSettings = ConfigurationManager.AppSettings;
            var path = appSettings[pathToPhotosKey];

            if (string.IsNullOrEmpty(appSettings[pathToPhotosKey]))
            {
                path = defaultValuePathToPhotos;
            }
            return path;
        }

        public string СheckValueFileExtensions()
        {
            var appSettings = ConfigurationManager.AppSettings;
            var permittedType = appSettings[fileExtensionsKey];

            if (string.IsNullOrEmpty(appSettings[fileExtensionsKey]))
            {
                permittedType = defaultValueFileExtensions;
            }
            return permittedType;
        }

        public static string DBConnectionString()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["SQLDB"] ?? throw new ArgumentException("SQL");
            return connectionString.ConnectionString;
        }
    }
}