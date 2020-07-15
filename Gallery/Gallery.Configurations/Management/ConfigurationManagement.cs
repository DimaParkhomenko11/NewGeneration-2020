using System;
using System.Configuration;

namespace Gallery.Configurations.Management
{
    public class ConfigurationManagement
    {
        //
        //Variables for storing key data
        private const string PathToPhotosKey = "PathToPhotos";
        private const string PathToTempPhotosKey = "PathToTempPhotos";
        private const string FileExtensionsKey = "FileExtensions";

        //
        //Default constants
        private const string DefaultValuePathToPhotos = "/Content/Images/";
        private const string DefaultValuePathToTempPhotos = "/Content/Temp/";
        private const string DefaultValueFileExtensions = "image/jpeg;image/png";

        public static string СheckValuePathToPhotos()//Adding a default value PathToPhotos
        {
            var appSettings = ConfigurationManager.AppSettings;

            var path = appSettings[PathToPhotosKey];

            if (string.IsNullOrEmpty(appSettings[PathToPhotosKey]))
            {
                path = DefaultValuePathToPhotos;
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

        public static string AzureMqConnectionString()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["AzureStorageConnectionString"] ?? throw new ArgumentException("RabbitMQ");
            return connectionString.ConnectionString;
        }
    }
}