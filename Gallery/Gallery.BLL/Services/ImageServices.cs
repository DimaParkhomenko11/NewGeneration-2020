using System;
using System.IO;
using System.Threading.Tasks;
using FileSystemStorage;
using Gallery.BLL.Contract;
using Gallery.BLL.Interfaces;
using Gallery.DAL.Interfaces;
using Gallery.MQ.Interfaces;

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


        public async Task<bool> UploadTempImageAsync(byte[] dateBytes, string path, UserDto userDto, string userPathImages)
        {
            var isTempUpload = _mediaProvider.Upload(dateBytes, path);
            var fileName = Path.GetFileName(path);
            var extension = Path.GetExtension(fileName);
            var uniqueIdentName = fileName.Replace(extension, "");
            var user = await _userRepository.FindUserAsync(userDto.UserEmail, userDto.UserPassword);
            var isTempMediaExist = await _mediaRepository.IsTempMediaExistAsync(uniqueIdentName);
            if (!isTempMediaExist)
            { 
                await _mediaRepository.AddTempMediaToDatabaseAsync(uniqueIdentName, true, isTempUpload, user, userPathImages);
            }
            await _mediaRepository.UpdateTempMediaProcessAsync(uniqueIdentName, true);
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
                if (user == null)
                {
                    return false;
                }
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
