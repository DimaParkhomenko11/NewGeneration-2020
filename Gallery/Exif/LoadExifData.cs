using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Gallery.Exif
{
    public class LoadExifData
    {
        public static string title;
        public static string dateCreation;
        public static string dateUpload;
        public static string cameraManufacturer;
        public static string modelOfCamera;
        public static string fileSize;


        public void LoadExif(string LoadExifPath)
        {
            try
            {
                FileInfo fileInfo = new FileInfo(LoadExifPath);

                FileStream fileStream = new FileStream(LoadExifPath, FileMode.Open);
                BitmapSource bitmapSource = BitmapFrame.Create(fileStream);
                BitmapMetadata bitmapMetadata = (BitmapMetadata)bitmapSource.Metadata;


                //
                //title from FileInfo
                if (string.IsNullOrEmpty(fileInfo.Name))
                    title = "Data not found";
                else
                    title = fileInfo.Name;
                //
                //DateUpload from FileInfo
                if (fileInfo.CreationTime == null)
                    dateUpload = "Data not found";
                else
                    dateUpload = fileInfo.CreationTime.ToString("dd.MM.yyyy HH:mm:ss");
                //
                //FileSize from FileInfo
                if (fileInfo.Length >= 1024)
                {
                    fileSize = Math.Round((fileInfo.Length / 1024f), 1).ToString() + " KB";
                    if ((fileInfo.Length / 1024f) >= 1024f)
                        fileSize = Math.Round((fileInfo.Length / 1024f) / 1024f, 2).ToString() + " MB";
                }
                else
                    fileSize = fileInfo.Length.ToString() + " B";


                if (!LoadExifPath.Contains(".jpg"))
                {
                    cameraManufacturer = "Data not found";
                    modelOfCamera = "Data not found";
                    dateCreation = "Data not found";
                    // MessageBox.Show("aaa"); 
                }
                else
                {
                    //
                    //manufacturer from EXIF
                    if (string.IsNullOrEmpty(bitmapMetadata.CameraManufacturer))
                        cameraManufacturer = "Data not found";
                    else
                        cameraManufacturer = bitmapMetadata.CameraManufacturer;


                    //
                    //modelOfCamera from EXIF
                    if (string.IsNullOrEmpty(bitmapMetadata.CameraModel))
                        modelOfCamera = "Data not found";
                    else
                        modelOfCamera = bitmapMetadata.CameraModel;


                    //
                    //DateCreation from EXIF
                    if (string.IsNullOrEmpty(bitmapMetadata.DateTaken))
                        dateCreation = "Data not found";
                    else
                        dateCreation = bitmapMetadata.DateTaken;
                }
                fileStream.Close();
            }
            catch (Exception err)
            {

                // Add error
            }


        }
    }
}