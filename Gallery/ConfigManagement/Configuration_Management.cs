using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace Gallery.ConfigManagement
{
    public class Configuration_Management
    {
        //
        //Variables for storing key data
        public string pathToPhotos { get; } = WebConfigurationManager.AppSettings["PathToPhotos"];
        public string fileExtensions { get; } = WebConfigurationManager.AppSettings["FileExtensions"];
        //
        //Default constants
        private const string defaultValuePathToPhotos = "/Content/Images/";
        private const string defaultValueFileExtensions = "image/jpeg;image/png";

        public string СheckValuePathToPhotos()//Adding a default value PathToPhotos
        {
            System.Configuration.Configuration configFile = null;
            if (System.Web.HttpContext.Current != null)
            {
                configFile = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");
            }
            else
            {
                configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            }
            var settings = configFile.AppSettings.Settings;
            if (settings["PathToPhotos"] == null)
            {
                settings.Add("PathToPhotos",defaultValuePathToPhotos);

                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
                return defaultValuePathToPhotos;
            }
            else
            {
                if (settings["PathToPhotos"].Value == "")
                {
                    settings["PathToPhotos"].Value = defaultValuePathToPhotos;
                    return defaultValuePathToPhotos;
                }
                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
                return pathToPhotos;
            }
            
        }

        public string СheckValueFileExtensions()//Adding a default value PathToPhotos
        {
            System.Configuration.Configuration configFile = null;
            if (System.Web.HttpContext.Current != null)
            {
                configFile = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");
            }
            else
            {
                configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            }
            var settings = configFile.AppSettings.Settings;
            if (settings["FileExtensions"] == null)
            {
                settings.Add("FileExtensions", defaultValueFileExtensions);

                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
                return defaultValueFileExtensions;
            }
            else
            {
                if (settings["FileExtensions"].Value == "")
                {
                    settings["FileExtensions"].Value = defaultValueFileExtensions;
                    return defaultValueFileExtensions;
                }
                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
                return fileExtensions;
            }
            
        }
    }
}
 