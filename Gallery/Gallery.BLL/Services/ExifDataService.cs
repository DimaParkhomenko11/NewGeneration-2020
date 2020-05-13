using System;
using System.IO;
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
            ExifDataService.DateUpload = fileInfo.CreationTime == null ? "Data not found" : fileInfo.CreationTime.ToString("dd.MM.yyyy HH:mm:ss");

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
                ExifDataService.CameraManufacturer = string.IsNullOrEmpty(bitmapMetadata.CameraManufacturer) ? "Data not found" : bitmapMetadata.CameraManufacturer;

                //modelOfCamera from EXIF
                ExifDataService.ModelOfCamera = string.IsNullOrEmpty(bitmapMetadata.CameraModel) ? "Data not found" : bitmapMetadata.CameraModel;

                //DateCreation from EXIF
                ExifDataService.DateCreation = string.IsNullOrEmpty(bitmapMetadata.DateTaken) ? "Data not found" : bitmapMetadata.DateTaken;
            }
            fileStream.Close();

        }
    }
}
