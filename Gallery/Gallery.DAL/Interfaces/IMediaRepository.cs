using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gallery.DAL.Models;

namespace Gallery.DAL.Interfaces
{
    public interface IMediaRepository
    {
        Task<bool> IsMediaExistAsync(string path);
        Task UpdateMediaDeleteStatusAsync(string path, bool newStatus);
        Task<Media> GetMediaByPathAsync(string path);
        Task AddMediaToDatabaseAsync(string name, string pathToMedia, User user, MediaType mediaType);

        Task<bool> IsTempMediaExistAsync(string uniqueIdentName);
        Task UpdateTempMediaProcessAsync(string uniqueIdentName, bool newStatusLoad);
        Task AddTempMediaToDatabaseAsync(string uniqueIdentName, bool inDuringLoading, bool isSuccess, User user, string userPathImages);
        Task<TemporaryMedia> GetTempMediaByLabelAndProgressLoadingAsync(string name, bool progressStatus);
        Task UpdateTemporaryMediaAsync(TemporaryMedia oldTemporaryMedia, TemporaryMedia newTemporaryMedia);


        Task<bool> IsMediaTypeExistAsync(string type);
        Task AddMediaTypeToDatabaseAsync(string type);
        Task<MediaType> GetMediaTypeAsync(string type);
    }
}
