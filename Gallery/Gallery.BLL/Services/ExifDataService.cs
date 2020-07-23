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
        public string GetTitle(string loadExifPath)
        {
            var fileInfo = new FileInfo(loadExifPath);

            return string.IsNullOrEmpty(fileInfo.Name) ? "Data not found" : fileInfo.Name;
        }

        public string GetDateUpload(string loadExifPath)
        {
            var fileInfo = new FileInfo(loadExifPath);

            return fileInfo.CreationTime == null ? "Data not found" : fileInfo.CreationTime.ToString("dd.MM.yyyy HH:mm:ss");
        }

        public string GetFileSize(string loadExifPath)
        {
            var fileInfo = new FileInfo(loadExifPath);

            string fileSize;
            if (fileInfo.Length >= 1024)
            {
                fileSize = Math.Round((fileInfo.Length / 1024f), 1).ToString() + " KB";
                if ((fileInfo.Length / 1024f) >= 1024f)
                {
                    fileSize = Math.Round((fileInfo.Length / 1024f) / 1024f, 2).ToString() + " MB";
                }
            }
            else
            {
                fileSize = fileInfo.Length.ToString() + " B";
            }

            return fileSize;
        }

        public string GetCameraManufacturer(string loadExifPath)
        {
            var fileInfo = new FileInfo(loadExifPath);

            if (!fileInfo.Extension.Equals(".jpg") && !fileInfo.Extension.Equals(".jpeg"))
                return "Data not found";
            using (var fileStream = new FileStream(loadExifPath, FileMode.Open))
            {
                var bitmapSource = BitmapFrame.Create(fileStream);

                var bitmapMetadata = (BitmapMetadata)bitmapSource.Metadata;

                return string.IsNullOrEmpty(bitmapMetadata.CameraManufacturer) ? "Data not found" : bitmapMetadata.CameraManufacturer;
            }
        }

        public string GetModelOfCamera(string loadExifPath)
        {
            var fileInfo = new FileInfo(loadExifPath);

            if (!fileInfo.Extension.Equals(".jpg") && !fileInfo.Extension.Equals(".jpeg"))
                return "Data not found";
            using (var fileStream = new FileStream(loadExifPath, FileMode.Open))
            {
                var bitmapSource = BitmapFrame.Create(fileStream);

                var bitmapMetadata = (BitmapMetadata)bitmapSource.Metadata;

                return string.IsNullOrEmpty(bitmapMetadata.CameraModel) ? "Data not found" : bitmapMetadata.CameraModel;
            }
        }

        public string GetDateCreation(string loadExifPath)
        {
            var fileInfo = new FileInfo(loadExifPath);

            if (!fileInfo.Extension.Equals(".jpg") && !fileInfo.Extension.Equals(".jpeg"))
                return "Data not found";
            using (var fileStream = new FileStream(loadExifPath, FileMode.Open))
            {
                var bitmapSource = BitmapFrame.Create(fileStream);

                var bitmapMetadata = (BitmapMetadata)bitmapSource.Metadata;

                return string.IsNullOrEmpty(bitmapMetadata.DateTaken) ? "Data not found" : bitmapMetadata.DateTaken;
            }
        }
    }
}
