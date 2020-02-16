using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Windows.Media.Imaging;
using System.Drawing;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;
using System.Windows;
using Gallery.ConfigManagement;
using System.Web.Configuration;
using Gallery.Exif;
using Gallery.Core;

namespace Gallery.Controllers
{
    public class HomeController : Controller
    {

        Configuration_Management Config = new Configuration_Management();
        Core.Service service = new Core.Service();
        
        //
        // Hash-Function
        // Input: String
        // Otput: String with ShaHash
        /*public static string ComputeSha256Hash(string rawData)
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
        }*/

        //
        // check for equality of pictures
        // Input: Bitmap1, Bitmap2
        // Output:
        //        true - is equal
        //        false - isn't equal
        //
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
            LoadExifData loadExif = new LoadExifData();
            loadExif.LoadExif(LoadExifPath);
        }

        
        [HttpPost]
        public ActionResult Delete(string PathFileDelete = "")
        {
            try
            {
                
                if (PathFileDelete.Replace(Config.pathToPhotos, "").Replace(Path.GetFileName(PathFileDelete), "").Replace("/", "") == service.ComputeSha256Hash(User.Identity.Name))
                {
                    if (PathFileDelete != "" && Directory.Exists(Server.MapPath(PathFileDelete.Replace(Path.GetFileName(PathFileDelete), ""))))
                        System.IO.File.Delete(Server.MapPath(PathFileDelete));
                    else
                    {
                        ViewBag.Error = "File not found!";
                        return View("Error");
                    }
                }
                else
                {
                    ViewBag.Error = "Authorisation Error!";
                    return View("Error");
                }
            }
            catch(Exception err)
            {
                ViewBag.Error = "Unexpected error: " + err.Message;
                return View("Error");
            }
            return RedirectToAction("Index");
        }


        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Error()
        {
            return View();
        }

      

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase files)
        {
            try
            {
                if (files != null)
                {
                    if (!string.IsNullOrEmpty(User.Identity.Name))
                    {
                        if (Config.fileExtensions.Contains(files.ContentType))
                        {
                            FileStream TempFileStream;
                            // Verify that the user selected a file and User is logged in
                            if (files.ContentLength > 0)
                            {
                                bool IsLoad = true;
                                // Encrypted User's directory path
                                string DirPath = Server.MapPath(Config.pathToPhotos) + service.ComputeSha256Hash(User.Identity.Name);

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
                                
                                if (!string.IsNullOrEmpty(DateTaken) || files.ContentType != "image/jpeg")
                                {
                                    if (Convert.ToDateTime(DateTaken) >= DateTime.Now.AddYears(-1) || files.ContentType != "image/jpeg")
                                    {
                                        TempFileStream = new FileStream(TempPath, FileMode.Open);
                                        Bitmap TempBmp = new Bitmap(TempFileStream);
                                        TempBmp = new Bitmap(TempBmp, 64, 64);
                                        TempFileStream.Close();

                                        // List of all Directories names
                                        List<string> dirsname = Directory.GetDirectories(Server.MapPath(Config.pathToPhotos)).ToList<string>();

                                        FileStream CheckFileStream;
                                        Bitmap CheckBmp;

                                        List<string> filesname;

                                        // foreach inside foreach in order to check a new photo for its copies in all folders of all users
                                        foreach (string dir in dirsname)
                                        {
                                            filesname = Directory.GetFiles(dir).ToList<string>();
                                            foreach (string fl in filesname)
                                            {
                                                CheckFileStream = new FileStream(fl, FileMode.Open);
                                                CheckBmp = new Bitmap(CheckFileStream);
                                                CheckBmp = new Bitmap(CheckBmp, 64, 64);

                                                CheckFileStream.Close();

                                                if (CompareBitmapsFast(TempBmp, CheckBmp))
                                                {
                                                    IsLoad = false;
                                                    ViewBag.Error = "Photo already exists!";
                                                    CheckBmp.Dispose();
                                                    break;
                                                }
                                                else
                                                    CheckBmp.Dispose();
                                            }
                                        }
                                    }
                                    else
                                    {
                                        ViewBag.Error = "Photo created more than a year ago!";
                                        IsLoad = false;
                                    }
                                }
                                else
                                {
                                    ViewBag.Error = "Photo creation date not found!";
                                    IsLoad = false;
                                }
                                
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
                        ViewBag.Error = "Log in please!";
                        return View("Error");
                    }
                }
                else
                {
                    
                    return View();
                }
            }
            catch (Exception err)
            {

                ViewBag.Error = "Unexpected error: " + err.Message;
                return View("Error");

            }
            return RedirectToAction("Index");
        }

        public ActionResult Upload()
        {
            return View();
        }

    }
}