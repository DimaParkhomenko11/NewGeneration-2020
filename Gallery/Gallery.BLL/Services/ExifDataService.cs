using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Gallery.BLL.Interfaces;

namespace Gallery.BLL.Services
{
    public class ExifDataService : IExifDataService
    {
        public static List<string> Title = new List<string>();
        public static List<string> DateCreation = new List<string>();
        public static List<string> DateUpload = new List<string>();
        public static List<string> CameraManufacturer = new List<string>();
        public static List<string> ModelOfCamera = new List<string>();
        public static List<string> FileSize = new List<string>();

        public async Task LoadExifDataAsync(string loadExifPath)
        {
            FileInfo fileInfo = new FileInfo(loadExifPath);

            //title from FileInfo
            ExifDataService.Title.Add(string.IsNullOrEmpty(fileInfo.Name) ? "Data not found" : fileInfo.Name);

            //DateUpload from FileInfo
            ExifDataService.DateUpload.Add(fileInfo.CreationTime == null ? "Data not found" : fileInfo.CreationTime.ToString("dd.MM.yyyy HH:mm:ss"));

            //FileSize from FileInfo
            if (fileInfo.Length >= 1024)
            {
                ExifDataService.FileSize.Add(Math.Round((fileInfo.Length / 1024f), 1).ToString() + " KB");
                if ((fileInfo.Length / 1024f) >= 1024f) ExifDataService.FileSize.Add(Math.Round((fileInfo.Length / 1024f) / 1024f, 2).ToString() + " MB");
            }
            else
            {
                ExifDataService.FileSize.Add(fileInfo.Length.ToString() + " B");
            }

            if (!fileInfo.Extension.Equals(".jpg") && !fileInfo.Extension.Equals(".jpeg"))
            {
                ExifDataService.CameraManufacturer.Add("Data not found");
                ExifDataService.ModelOfCamera.Add("Data not found");
                ExifDataService.DateCreation.Add("Data not found");
            }
            else
            {
                using (FileStream fs = new FileStream(loadExifPath, FileMode.Open))
                {
                    var bitmapSource = BitmapFrame.Create(fs);

                    var bitmapMetadata = (BitmapMetadata)bitmapSource.Metadata;

                    //manufacturer from EXIF
                    ExifDataService.CameraManufacturer.Add(string.IsNullOrEmpty(bitmapMetadata.CameraManufacturer) ? "Data not found" : bitmapMetadata.CameraManufacturer); 

                    //modelOfCamera from EXIF
                    ExifDataService.ModelOfCamera.Add(string.IsNullOrEmpty(bitmapMetadata.CameraModel) ? "Data not found" : bitmapMetadata.CameraModel);

                    //DateCreation from EXIF
                    ExifDataService.DateCreation.Add(string.IsNullOrEmpty(bitmapMetadata.DateTaken) ? "Data not found" : bitmapMetadata.DateTaken);
                }
            }
        }
    }
}
