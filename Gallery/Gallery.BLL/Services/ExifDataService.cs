using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Gallery.BLL.Interfaces;

namespace Gallery.BLL.Services
{
    public class ExifDataService : IExifDataService
    {
        public static string Title { get; set; }
        public static string DateCreation { get; set; }
        public static string DateUpload { get; set; }
        public static string CameraManufacturer { get; set; }
        public static string ModelOfCamera { get; set; }
        public static string FileSize { get; set; }

        public async Task LoadExifDataAsync(string LoadExifPath)
        {
            FileInfo fileInfo = new FileInfo(LoadExifPath);
            FileStream fileStream = new FileStream(LoadExifPath, FileMode.Open);
            BitmapSource bitmapSource = BitmapFrame.Create(fileStream);
            BitmapMetadata bitmapMetadata = (BitmapMetadata)bitmapSource.Metadata;

            //title from FileInfo
            if (string.IsNullOrEmpty(fileInfo.Name))
                ExifDataService.Title = "Data not found";
            else
                ExifDataService.Title = fileInfo.Name;

            //DateUpload from FileInfo
            if (fileInfo.CreationTime == null)
                ExifDataService.DateUpload = "Data not found";
            else
                ExifDataService.DateUpload = fileInfo.CreationTime.ToString("dd.MM.yyyy HH:mm:ss");

            //FileSize from FileInfo
            if (fileInfo.Length >= 1024)
            {
                ExifDataService.FileSize = Math.Round((fileInfo.Length / 1024f), 1).ToString() + " KB";
                if ((fileInfo.Length / 1024f) >= 1024f) ExifDataService.FileSize = Math.Round((fileInfo.Length / 1024f) / 1024f, 2).ToString() + " MB";
            }
            else
            {
                ExifDataService.FileSize = fileInfo.Length.ToString() + " B";
            }

            if (!LoadExifPath.Contains(".jpg"))
            {
                ExifDataService.CameraManufacturer = "Data not found";
                ExifDataService.ModelOfCamera = "Data not found";
                ExifDataService.DateCreation = "Data not found";
                // MessageBox.Show("aaa"); 
            }
            else
            {

                //manufacturer from EXIF
                if (string.IsNullOrEmpty(bitmapMetadata.CameraManufacturer))
                    ExifDataService.CameraManufacturer = "Data not found";
                else
                    ExifDataService.CameraManufacturer = bitmapMetadata.CameraManufacturer;

                //modelOfCamera from EXIF
                if (string.IsNullOrEmpty(bitmapMetadata.CameraModel))
                    ExifDataService.ModelOfCamera = "Data not found";
                else
                    ExifDataService.ModelOfCamera = bitmapMetadata.CameraModel;

                //DateCreation from EXIF
                if (string.IsNullOrEmpty(bitmapMetadata.DateTaken))
                    ExifDataService.DateCreation = "Data not found";
                else
                    ExifDataService.DateCreation = bitmapMetadata.DateTaken;
            }
            fileStream.Close();

        }
    }
}
