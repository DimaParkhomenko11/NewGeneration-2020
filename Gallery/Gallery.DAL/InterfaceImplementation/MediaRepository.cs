using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gallery.DAL.Interfaces;
using Gallery.DAL.Models;

namespace Gallery.DAL.InterfaceImplementation
{
    public class MediaRepository : IMediaRepository
    {
        private readonly SqlDbContext dbContext;

        public MediaRepository(SqlDbContext context)
        {
            dbContext = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<bool> IsMediaExistAsync(string path)
        {
            return await dbContext.Media.AnyAsync(t =>t.PathToMedia == path);
        }

        public async Task UpdateMediaDeleteStatusAsync(string path, bool newStatus)
        {
            var media = await dbContext.Media.FirstOrDefaultAsync(m => m.PathToMedia == path);
            media.isDeleted = newStatus;
            await dbContext.SaveChangesAsync();
        }

        public async Task<Media> GetMediaByPathAsync(string path)
        {
            return await dbContext.Media.FirstOrDefaultAsync(m => m.PathToMedia == path);
        }

        public async Task AddMediaToDatabaseAsync(string name, string pathToMedia, User user, MediaType mediaType)
        {
            dbContext.Media.Add(new Media
            {
                Id = 1,
                Name = name,
                PathToMedia = pathToMedia,
                isDeleted = false,
                UserId = user.Id,
                MediaTypeId = mediaType.Id
            });
            await dbContext.SaveChangesAsync();
        }

        public async Task<bool> IsTempMediaExistAsync(string uniqueIdentName)
        {
            return await dbContext.TemporaryMedia.AnyAsync(m => m.UniqueIdentName == uniqueIdentName);
        }

        public async Task UpdateTempMediaProcessAsync(string uniqueIdentName, bool newStatusLoad)
        {
            var tempMedia = await dbContext.TemporaryMedia.FirstOrDefaultAsync(m => m.UniqueIdentName == uniqueIdentName);
            tempMedia.InDuringLoading = newStatusLoad;
            await dbContext.SaveChangesAsync();
        }

        public async Task AddTempMediaToDatabaseAsync(string uniqueIdentName, bool inDuringLoading, bool isSuccess, User user)
        {
            dbContext.TemporaryMedia.Add(new TemporaryMedia
            {
                UniqueIdentName = uniqueIdentName,
                InDuringLoading = inDuringLoading,
                IsSuccess = isSuccess,
                UserId = user.Id
            });
            await dbContext.SaveChangesAsync();
        }

        public async Task<bool> IsMediaTypeExistAsync(string type)
        {
            return await dbContext.MediaTypes.AnyAsync(m => m.Type == type);
        }

        public async Task AddMediaTypeToDatabaseAsync(string type)
        {
             dbContext.MediaTypes.Add(new MediaType
             {
                 Type = type
             });
             await dbContext.SaveChangesAsync();
        }

        public async Task<MediaType> GetMediaTypeAsync(string type)
        {
            return await dbContext.MediaTypes.FirstOrDefaultAsync(m => m.Type == type);
        }
    }
}
