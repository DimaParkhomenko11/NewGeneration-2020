using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Autofac.Integration.WebApi;
using Gallery.BLL.Contract;
using Gallery.BLL.Interfaces;
using Gallery.BLL.Services;
using Gallery.Configurations.Management;
using Gallery.DAL.InterfaceImplementation;
using Gallery.DAL.Interfaces;
using Gallery.DAL.Models;
using Gallery.MQ.Abstraction;
using Gallery.MSMQ;
using Gallery.MSMQ.Implementation;
using Gallery.RMQ;
using Gallery.RMQ.Implementation;
using Microsoft.Ajax.Utilities;

namespace Gallery.Controllers
{
    public class HomeController : Controller
    {

        private readonly IHashService _hashService;
        private readonly IImagesService _imagesService;
        private readonly IUsersService _usersService;
        private readonly INamingService _namingService;
        private readonly PublisherMQ _publisher;
        private readonly IExifDataService _exifDataService;
        

        public HomeController(IImagesService imageService, IHashService hashService, IUsersService usersService, INamingService namingService, PublisherMQ publisher, IExifDataService exifDataService)
        {
            _imagesService = imageService ?? throw new ArgumentNullException(nameof(imageService));
            _hashService = hashService ?? throw new ArgumentNullException(nameof(hashService));
            _usersService = usersService ?? throw new ArgumentNullException(nameof(usersService));
            _namingService = namingService ?? throw new ArgumentNullException(nameof(namingService));
            _publisher = publisher ?? throw new ArgumentNullException(nameof(publisher));
            _exifDataService = exifDataService ?? throw new ArgumentNullException(nameof(exifDataService));
        }


        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Delete(string PathFileDelete)
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
                    var userHash = PathFileDelete.Replace(ConfigurationManagement.СheckValuePathToPhotos(), "")
                        .Replace(Path.GetFileName(PathFileDelete), "").Replace("/", "");

                    if (userHash == _hashService.ComputeSha256Hash(User.Identity.Name))
                    {
                        await _imagesService.DeleteFileAsync(fullPath);
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
                    if (ConfigurationManagement.СheckValueFileExtensions().Contains(files.ContentType))
                    {
                        // Verify that the user selected a file and User is logged in
                        if (files.ContentLength > 0)
                        {
                            byte[] data;
                            using (MemoryStream memoryStream = new MemoryStream())
                            {
                                files.InputStream.CopyTo(memoryStream);
                                data = memoryStream.ToArray();
                            }
                            var filename = _namingService.NameCleaner(files.FileName);
                            var uniqueIdentName = Guid.NewGuid();
                            var extension = Path.GetExtension(files.FileName);
                            // Encrypted User's directory path
                            var tempDirPath = Server.MapPath(ConfigurationManagement.СheckValuePathToTempPhotos());
                            var userDirPath = Server.MapPath(ConfigurationManagement.СheckValuePathToPhotos()) + _hashService.ComputeSha256Hash(User.Identity.Name);
                            var directoryExists = Directory.Exists(tempDirPath);
                            if (!directoryExists)
                            {
                                Directory.CreateDirectory(tempDirPath);
                            }
                            var tempFilePath = Path.Combine(tempDirPath, uniqueIdentName.ToString() + extension);
                            var userFilePath = Path.Combine(userDirPath, filename);
                            var userDto = await _usersService.GetUserByIdAsync(Convert.ToInt32(User.Identity.Name));
                            if (userDto == null)
                            {
                                ViewBag.Error = "Oops, something went wrong.";
                                return View("Error");
                            }

                            var doneUpload = await _imagesService.UploadTempImageAsync(data, tempFilePath, userDto, userFilePath);
                            if (!doneUpload)
                            {
                                ViewBag.Error = "Oops, something went wrong.";
                                return View("Error");
                            }
                            var parserQueue = new ParserMQ().ParserMq();
                            var messageDto = new MessageDto
                            {
                                UniqueName = uniqueIdentName.ToString(),
                                TempPath = tempFilePath,
                                UserId = userDto.UserId,
                                UserPath = userFilePath
                            };
                            _publisher.PublishMessage(messageDto, parserQueue["queue:upload-image"]);
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

        public async Task<ActionResult> Index()
        {
            var pathToPhotos = ConfigurationManagement.СheckValuePathToPhotos();
            var pathToTempPhotos = ConfigurationManagement.СheckValuePathToTempPhotos();

            var fullPathToPhotos = Server.MapPath(pathToPhotos);
            var fullPathToTempPhotos = Server.MapPath(pathToTempPhotos);

            if (!Directory.Exists(fullPathToPhotos))
            {
                Directory.CreateDirectory(fullPathToPhotos);
            }
            if (!Directory.Exists(fullPathToTempPhotos))
            {
                Directory.CreateDirectory(fullPathToTempPhotos);
            }

            if (Request.IsAuthenticated)
            {
                string fullPathToUserPhotos = fullPathToPhotos + _hashService.ComputeSha256Hash(User.Identity.Name);
                if (!Directory.Exists(fullPathToUserPhotos))
                {
                    Directory.CreateDirectory(fullPathToUserPhotos);
                }
                var userId = Convert.ToInt32(User.Identity.Name);
                var user = await _usersService.GetUserByIdAsync(userId);
                ViewData["Name"] = _namingService.UserNameCleaner(user.UserEmail);
            }
            var imgDirsNames = Directory.GetDirectories(fullPathToPhotos);
            ViewBag.Titles = (from directory in imgDirsNames from file in Directory.GetFiles(directory) select _exifDataService.GetTitle(file)).ToList();
            ViewBag.DateUpload = (from directory in imgDirsNames from file in Directory.GetFiles(directory) select _exifDataService.GetDateUpload(file)).ToList();
            ViewBag.FileSize = (from directory in imgDirsNames from file in Directory.GetFiles(directory) select _exifDataService.GetFileSize(file)).ToList();
            ViewBag.ModelOfCamera = (from directory in imgDirsNames from file in Directory.GetFiles(directory) select _exifDataService.GetModelOfCamera(file)).ToList();
            ViewBag.DateCreation = (from directory in imgDirsNames from file in Directory.GetFiles(directory) select _exifDataService.GetDateCreation(file)).ToList();
            ViewBag.CameraManufacturer = (from directory in imgDirsNames from file in Directory.GetFiles(directory) select _exifDataService.GetCameraManufacturer(file)).ToList();
            return View();
        }

        [Authorize]
        public async Task<ActionResult> Upload()
        {
            var userId = Convert.ToInt32(User.Identity.Name);
            var user = await _usersService.GetUserByIdAsync(userId);
            ViewData["Name"] = _namingService.UserNameCleaner(user.UserEmail);
            return View();
        }
    }
}