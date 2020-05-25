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

        Task<bool> IsMediaTypeExistAsync(string type);
        Task AddMediaTypeToDatabaseAsync(string type);
        Task<MediaType> GetMediaTypeAsync(string type);
    }
}
