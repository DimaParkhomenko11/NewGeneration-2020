using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using FileSystemStorage;
using Gallery.BLL.Contract;
using Gallery.BLL.Interfaces;
using Gallery.DAL.Interfaces;

namespace Gallery.BLL.Services
{
    public class ImageServices : IImagesService
    {
        private readonly IMediaProvider _mediaProvider;
        private readonly IMediaRepository _mediaRepository;
        private readonly IRepository _userRepository;

        public ImageServices(IMediaProvider mediaProvider, IMediaRepository mediaRepository, IRepository userRepository)
        {
            _mediaProvider = mediaProvider ?? throw new ArgumentNullException(nameof(mediaProvider));
            _mediaRepository = mediaRepository ?? throw new ArgumentNullException(nameof(mediaRepository));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }


        public Task<bool> UploadTempImageAsync(byte[] dateBytes, string path, UserDto userDto)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UploadImageAsync(byte[] dateBytes, string path, UserDto userDto)
        {
            var isMediaExistAsync = await _mediaRepository.IsMediaExistAsync(path);
            if (isMediaExistAsync)
            {
                var media = await _mediaRepository.GetMediaByPathAsync(path);
                if (media.isDeleted)
                { 
                    await _mediaRepository.UpdateMediaDeleteStatusAsync(path, false);
                }
            }
            else
            {
                var typeExtension = Path.GetExtension(path);
                var user = await _userRepository.FindUserAsync(userDto.UserEmail, userDto.UserPassword);
                var isMediaTypeExist = await _mediaRepository.IsMediaTypeExistAsync(typeExtension);
                if (!isMediaTypeExist)
                {
                    await _mediaRepository.AddMediaTypeToDatabaseAsync(typeExtension);
                }
                var mediaType = await _mediaRepository.GetMediaTypeAsync(typeExtension);
                var fileName = Path.GetFileName(path);
                await _mediaRepository.AddMediaToDatabaseAsync(fileName, path, user, mediaType);
            }
            return _mediaProvider.Upload(dateBytes, path);
        }

        public async Task<bool> DeleteFileAsync(string path)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException(nameof(path));
            var isMediaExist = await _mediaRepository.IsMediaExistAsync(path);
            if (isMediaExist)
            {
                var media = await _mediaRepository.GetMediaByPathAsync(path);
                if (!media.isDeleted)
                    await _mediaRepository.UpdateMediaDeleteStatusAsync(path, true);
            }
            return _mediaProvider.Delete(path);
        }

        public byte[] ReadFile(string path)
        {
            return _mediaProvider.Read(path);
        }

        public string NameCleaner(string fileName)
        {
            try
            {
                return Regex.Replace(fileName, @"[^\w\.@-]", "",
                    RegexOptions.None, TimeSpan.FromSeconds(1.5));
            }
            catch (RegexMatchTimeoutException)
            {
                return String.Empty;
            }
        }
    }
}
