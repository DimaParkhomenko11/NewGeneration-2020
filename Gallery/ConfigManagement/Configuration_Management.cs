using System;
using System.Collections.Generic;
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

        public static void AddDefaultValueAppSettings(string key, string value)//Adding a default value  
        {  
            try  
            {
                var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var settings = configFile.AppSettings.Settings;
                if (settings[key] == null)
                {
                    settings.Add(key, value);
                }
                else
                {
                    if (settings[key].Value == "")
                    {
                        settings[key].Value = value;
                    }
                }
                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
            }  
            catch (ConfigurationErrorsException)  
            {
                //Add error
            }
        } 
    }
}