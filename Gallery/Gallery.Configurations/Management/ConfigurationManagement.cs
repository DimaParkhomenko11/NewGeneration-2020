using System;
using System.Configuration;

namespace Gallery.Configurations.Management
{
    public class ConfigurationManagement
    {
        //
        //Variables for storing key data
        private const string PathToUserPhotosKey = "PathToUserPhotos";
        private const string PathToTempPhotosKey = "PathToTempPhotos";
        private const string FileExtensionsKey = "FileExtensions";
        private const string MessageQueuingPathKey = "MessageQueuingPath";
        private const string RabbitMqKey = "RabbitMQ";

        //
        //Default constants
        private const string DefaultValuePathToUserPhotos = "/Content/Images/";
        private const string DefaultValuePathToTempPhotos = "/Content/Temp/";
        private const string DefaultValueFileExtensions = "image/jpeg;image/png";
        private const string DefaultValueMessageQueuingPath = @".\private$\GalleryMQ";

        public static string СheckValuePathToUserPhotos()//Adding a default value PathToPhotos
        {
            var appSettings = ConfigurationManager.AppSettings;

            var path = appSettings[PathToUserPhotosKey];

            if (string.IsNullOrEmpty(appSettings[PathToUserPhotosKey]))
            {
                path = DefaultValuePathToUserPhotos;
            }
            return path;
        }

        public static string СheckValuePathToTempPhotos()//Adding a default value PathToPhotos
        {
            var path = ConfigurationManager.AppSettings[PathToTempPhotosKey];

            if (string.IsNullOrEmpty(ConfigurationManager.AppSettings[PathToTempPhotosKey]))
            {
                path = DefaultValuePathToTempPhotos;
            }
            return path;
        }

        public static string СheckValueFileExtensions()
        {
            var permittedType = ConfigurationManager.AppSettings[FileExtensionsKey];

            if (string.IsNullOrEmpty(ConfigurationManager.AppSettings[FileExtensionsKey]))
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

        public static string RabbitMqConnectionString()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["RabbitMQ"] ?? throw new ArgumentException("RabbitMQ");
            return connectionString.ConnectionString;
        }
    }
}