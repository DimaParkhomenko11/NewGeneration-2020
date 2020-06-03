using System;
using System.Collections.Generic;
using System.Configuration;

namespace Gallery.ConfigManagement
{
    public class ConfigurationManagement
    {
        //
        //Variables for storing key data
        public string PathToUserPhotosKey { get; } = "PathToUserPhotos";
        public string PathToTempPhotosKey { get; } = "PathToTempPhotos";
        public string FileExtensionsKey { get; } = "FileExtensions";
        public string MessageQueuingPathKey { get; } = "MessageQueuingPath";

        //
        //Default constants
        private const string DefaultValuePathToUserPhotos = "/Content/Images/";
        private const string DefaultValuePathToTempPhotos = "/Content/Temp/";
        private const string DefaultValueFileExtensions = "image/jpeg;image/png";
        private const string DefaultValueMessageQueuingPath = ".\\private$\\GalleryMQ";

        public string СheckValuePathToUserPhotos()//Adding a default value PathToPhotos
        {
            var appSettings = ConfigurationManager.AppSettings;
            var path = appSettings[PathToUserPhotosKey];

            if (string.IsNullOrEmpty(appSettings[PathToUserPhotosKey]))
            {
                path = DefaultValuePathToUserPhotos;
            }
            return path;
        }

        public string СheckValuePathToTempPhotos()//Adding a default value PathToPhotos
        {
            var appSettings = ConfigurationManager.AppSettings;
            var path = appSettings[PathToTempPhotosKey];

            if (string.IsNullOrEmpty(appSettings[PathToTempPhotosKey]))
            {
                path = DefaultValuePathToTempPhotos;
            }
            return path;
        }

        public string СheckValuePathToMessageQueuing()
        {
            var appSettings = ConfigurationManager.AppSettings;
            var path = appSettings[MessageQueuingPathKey];

            if (string.IsNullOrEmpty(appSettings[MessageQueuingPathKey]))
            {
                path = DefaultValueMessageQueuingPath;
            }
            return path;
        }

        public string СheckValueFileExtensions()
        {
            var appSettings = ConfigurationManager.AppSettings;
            var permittedType = appSettings[FileExtensionsKey];

            if (string.IsNullOrEmpty(appSettings[FileExtensionsKey]))
            {
                permittedType = DefaultValueFileExtensions;
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