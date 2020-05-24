using System;
using System.Collections.Generic;
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


    }
}
