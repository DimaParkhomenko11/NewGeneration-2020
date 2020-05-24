using System;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Autofac.Integration.WebApi;
using Gallery.ConfigManagement;
using Gallery.BLL.Interfaces;
using Gallery.DAL.InterfaceImplementation;
using Gallery.DAL.Interfaces;
using Gallery.DAL.Models;
using Microsoft.Ajax.Utilities;

namespace Gallery.Controllers
{
    public class HomeController : Controller
    {

        private IHashService _hashService;
        private IImagesService _imagesService;
        private IUsersService _usersService;
        private readonly ConfigurationManagement _config;

        public HomeController(IImagesService imageService, IHashService hashService, ConfigurationManagement config, IUsersService usersService)
        {
            _imagesService = imageService ?? throw new ArgumentNullException(nameof(imageService));
            _hashService = hashService ?? throw new ArgumentNullException(nameof(hashService));
            _config = config ?? throw new ArgumentNullException(nameof(config));
            _usersService = usersService ?? throw new ArgumentNullException(nameof(usersService));

        }


        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult Upload()
        {
            ViewBag.Message = User.Identity.GetUserRole();
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public ActionResult Delete(string PathFileDelete)
        {
            try
            {
                var fullPath = Server.MapPath(PathFileDelete);
                var isExists = System.IO.File.Exists(fullPath);
                if (PathFileDelete.IsNullOrWhiteSpace() || !isExists)
                {
                    ViewBag.Error = "File not found!";
                    return View("Error");
                }
                else
                {
                    var userHash = PathFileDelete.Replace(_config.СheckValuePathToPhotos(), "")
                        .Replace(Path.GetFileName(PathFileDelete), "").Replace("/", "");

                    if (userHash == _hashService.ComputeSha256Hash(User.Identity.Name))
                    {
                        _imagesService.DeleteFile(fullPath);
                    }
                    else
                    {
                        ViewBag.Error = "Authorisation Error!";
                        return View("Error");
                    }
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
        [Authorize]
        public async Task<ActionResult> Upload(HttpPostedFileBase files)
        {
            try
            {
                if (files != null)
                {
                    if (_config.СheckValueFileExtensions().Contains(files.ContentType))
                    {
                        // Verify that the user selected a file and User is logged in
                        if (files.ContentLength > 0)
                        {

                            MemoryStream memoryStream = new MemoryStream();
                            files.InputStream.CopyTo(memoryStream);
                            byte[] data = memoryStream.ToArray();
                            var filename = _imagesService.NameCleaner(files.FileName);

                            // Encrypted User's directory path
                            string DirPath = Server.MapPath(_config.СheckValuePathToPhotos()) + _hashService.ComputeSha256Hash(User.Identity.Name);
                            string filePath = Path.Combine(DirPath, filename);




                            bool doneUpload = _imagesService.UploadFile(data, filePath);


                            ViewBag.Error = "Photo already exists";
                            return View("Error");


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
            }
            catch (Exception err)
            {

                ViewBag.Error = "Unexpected error: " + err.Message;
                return View("Error");

            }
            return RedirectToAction("Index");
        }
    }
}