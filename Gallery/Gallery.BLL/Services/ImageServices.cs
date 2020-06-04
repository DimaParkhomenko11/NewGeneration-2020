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
        private readonly IPublisher _publisher;

        public ImageServices(IMediaProvider mediaProvider, IMediaRepository mediaRepository, IRepository userRepository, IPublisher publisher)
        {
            _mediaProvider = mediaProvider ?? throw new ArgumentNullException(nameof(mediaProvider));
            _mediaRepository = mediaRepository ?? throw new ArgumentNullException(nameof(mediaRepository));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _publisher = publisher ?? throw new ArgumentNullException(nameof(publisher));
        }


        public async Task<bool> UploadTempImageAsync(byte[] dateBytes, string path, UserDto userDto)
        {
            var isTempUpload = _mediaProvider.Upload(dateBytes, path);
            var fileName = Path.GetFileName(path);
            var user = await _userRepository.FindUserAsync(userDto.UserEmail, userDto.UserPassword);
            var isTempMediaExist = await _mediaRepository.IsTempMediaExistAsync("name1");
            if (!isTempMediaExist)
            { 
                await _mediaRepository.AddTempMediaToDatabaseAsync("name1", true, isTempUpload, user);
            }
            await _mediaRepository.UpdateTempMediaProcessAsync("name1", true);
            // _publisher.PublishMessage(dateBytes, pathMessage, "name1");
            return isTempUpload;

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
    }
}
