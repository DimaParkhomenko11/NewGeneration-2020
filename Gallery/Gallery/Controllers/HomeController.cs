using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Gallery.ConfigManagement;
using System.Windows.Media.Imaging;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace Gallery.Controllers
{
    public class HomeController : Controller
    {
        public static string Title { get; set; }
        public static string DateCreation { get; set; }
        public static string DateUpload { get; set; }
        public static string CameraManufacturer { get; set; }
        public static string ModelOfCamera { get; set; }
        public static string FileSize { get; set; }

        private readonly ConfigurationManagement Config = new ConfigurationManagement();

        public ActionResult Index()
        {
            return View();
        }



        //Hash-Function
        //Input: String
        //Output: String with ShaHash
        public string ComputeSha256Hash(string rawData)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }


        public static bool CompareBitmapsFast(Bitmap bmp1, Bitmap bmp2)
        {
            if (bmp1 == null || bmp2 == null)
                return false;
            if (object.Equals(bmp1, bmp2))
                return true;
            if (!bmp1.Size.Equals(bmp2.Size) || !bmp1.PixelFormat.Equals(bmp2.PixelFormat))
                return false;

            int bytes = bmp1.Width * bmp1.Height * (Image.GetPixelFormatSize(bmp1.PixelFormat) / 8);

            bool result = true;
            byte[] b1bytes = new byte[bytes];
            byte[] b2bytes = new byte[bytes];

            BitmapData bitmapData1 = bmp1.LockBits(new Rectangle(0, 0, bmp1.Width, bmp1.Height), ImageLockMode.ReadOnly, bmp1.PixelFormat);
            BitmapData bitmapData2 = bmp2.LockBits(new Rectangle(0, 0, bmp2.Width, bmp2.Height), ImageLockMode.ReadOnly, bmp2.PixelFormat);

            Marshal.Copy(bitmapData1.Scan0, b1bytes, 0, bytes);
            Marshal.Copy(bitmapData2.Scan0, b2bytes, 0, bytes);

            for (int n = 0; n <= bytes - 1; n++)
            {
                if (b1bytes[n] != b2bytes[n])
                {
                    result = false;
                    break;
                }
            }

            bmp1.UnlockBits(bitmapData1);
            bmp2.UnlockBits(bitmapData2);

            return result;
        }

        

        public static void LoadExifData(string LoadExifPath)
        {
            FileInfo fileInfo = new FileInfo(LoadExifPath);
            FileStream fileStream = new FileStream(LoadExifPath, FileMode.Open);
            BitmapSource bitmapSource = BitmapFrame.Create(fileStream);
            BitmapMetadata bitmapMetadata = (BitmapMetadata)bitmapSource.Metadata;

            //title from FileInfo
            if (string.IsNullOrEmpty(fileInfo.Name))
                Title = "Data not found";
            else
                Title = fileInfo.Name;

            //DateUpload from FileInfo
            if (fileInfo.CreationTime == null)
                DateUpload = "Data not found";
            else
                DateUpload = fileInfo.CreationTime.ToString("dd.MM.yyyy HH:mm:ss");
            
            //FileSize from FileInfo
            if (fileInfo.Length >= 1024)
            {
                FileSize = Math.Round((fileInfo.Length / 1024f), 1).ToString() + " KB";
                if ((fileInfo.Length / 1024f) >= 1024f)
                    FileSize = Math.Round((fileInfo.Length / 1024f) / 1024f, 2).ToString() + " MB";
            }
            else
            {
                FileSize = fileInfo.Length.ToString() + " B";
            }

            if (!LoadExifPath.Contains(".jpg"))
            {
                CameraManufacturer = "Data not found";
                ModelOfCamera = "Data not found";
                DateCreation = "Data not found";
                // MessageBox.Show("aaa"); 
            }
            else
            {
                
                //manufacturer from EXIF
                if (string.IsNullOrEmpty(bitmapMetadata.CameraManufacturer))
                    CameraManufacturer = "Data not found";
                else
                    CameraManufacturer = bitmapMetadata.CameraManufacturer;
                
                //modelOfCamera from EXIF
                if (string.IsNullOrEmpty(bitmapMetadata.CameraModel))
                    ModelOfCamera = "Data not found";
                else
                    ModelOfCamera = bitmapMetadata.CameraModel;
                
                //DateCreation from EXIF
                if (string.IsNullOrEmpty(bitmapMetadata.DateTaken))
                    DateCreation = "Data not found";
                else
                    DateCreation = bitmapMetadata.DateTaken;
            }
            fileStream.Close();

        }

         [HttpPost]
         public ActionResult Delete(string PathFileDelete = "")
         {
             try
             {
                 if (PathFileDelete != "")
                 { 
                     System.IO.File.Delete(Server.MapPath(PathFileDelete));
                 }
                 else
                 {
                     ViewBag.Error = "File not found!";
                     return View("Error");
                 }
             }
             catch (Exception err)
             {
                 ViewBag.Error = "Unexpected error: " + err.Message;
                 return View("Error");
             }
             return RedirectToAction("Index");
         }



        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase files)
        {
            if (files != null)
            {
                if (Config.СheckValueFileExtensions().Contains(files.ContentType))
                {
                    FileStream TempFileStream;
                    // Verify that the user selected a file and User is logged in
                    if (files.ContentLength > 0)
                    {
                        bool IsLoad = true;
                        // Encrypted User's directory path
                        string DirPath = Server.MapPath(Config.СheckValuePathToPhotos());

                        // extract only the filename
                        var fileName = Path.GetFileName(files.FileName);
                        // store the file inside ~/Content/Temp folder
                        var TempPath = Path.Combine(Server.MapPath("~/Content/Temp"), fileName);
                        files.SaveAs(TempPath);
                        TempFileStream = new FileStream(TempPath, FileMode.Open);
                        BitmapSource bitmapSource = BitmapFrame.Create(TempFileStream);
                        BitmapMetadata bitmapMetadata = (BitmapMetadata)bitmapSource.Metadata;
                        var DateTaken = bitmapMetadata.DateTaken;
                        TempFileStream.Close();


                        if (IsLoad)
                        {
                            // extract only the filename
                            var OriginalFileName = Path.GetFileName(files.FileName);
                            // store the file inside User's folder
                            var OriginalPath = Path.Combine(DirPath, OriginalFileName);
                            //System.Windows.MessageBox.Show(OriginalPath);
                            files.SaveAs(OriginalPath);
                            System.IO.File.Delete(TempPath);
                        }
                        else
                        {
                            System.IO.File.Delete(TempPath);
                            return View("Error");
                        }

                    }
                    else
                    {
                        ViewBag.Error = "File too small!";
                        return View("Error");
                    }
                    // redirect back to the index action to show the form once again
                }
                else
                {
                    ViewBag.Error = "Inappropriate format!";
                    return View("Error");
                }
            }
            else
            {

                return View();
            }
            return RedirectToAction("Index");
        }


        public ActionResult Upload()
        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


      
    }

}