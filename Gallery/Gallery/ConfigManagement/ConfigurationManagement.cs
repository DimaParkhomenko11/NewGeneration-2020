using System;
using System.Collections.Generic;
using System.Configuration;

namespace Gallery.ConfigManagement
{
    public class ConfigurationManagement
    {
        //
        //Variables for storing key data
        public string pathToUserPhotosKey { get; } = "PathToUserPhotos";
        public string pathToTempPhotosKey { get; } = "PathToTempPhotos";
        public string fileExtensionsKey { get; } = "FileExtensions";
        //
        //Default constants
        private const string defaultValuePathToUserPhotos = "/Content/Images/";
        private const string defaultValuePathToTempPhotos = "/Content/Temp/";
        private const string defaultValueFileExtensions = "image/jpeg;image/png";

        public string СheckValuePathToUserPhotos()//Adding a default value PathToPhotos
        {
            var appSettings = ConfigurationManager.AppSettings;
            var path = appSettings[pathToUserPhotosKey];

            if (string.IsNullOrEmpty(appSettings[pathToUserPhotosKey]))
            {
                path = defaultValuePathToUserPhotos;
            }
            return path;
        }

        public string СheckValuePathToTempPhotos()//Adding a default value PathToPhotos
        {
            var appSettings = ConfigurationManager.AppSettings;
            var path = appSettings[pathToTempPhotosKey];

            if (string.IsNullOrEmpty(appSettings[pathToTempPhotosKey]))
            {
                path = defaultValuePathToTempPhotos;
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