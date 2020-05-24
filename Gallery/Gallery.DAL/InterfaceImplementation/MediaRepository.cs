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
            return await dbContext.Media.AnyAsync(m => m.PathToMedia == path);
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

        public async Task AddMediaToDatabaseAsync(string name, string pathToMedia, int userId, int mediaTypeId)
        {
            dbContext.Media.Add(new Media
            {
                Name = name,
                PathToMedia = pathToMedia,
                isDeleted = false,
                UserId = userId,
                MediaTypeId = mediaTypeId
            });
            await dbContext.SaveChangesAsync();
        }
    }
}
