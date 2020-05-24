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
            var media = dbContext.Media.FirstOrDefault(m => m.PathToMedia == path);
            media.isDeleted = newStatus;
            await dbContext.SaveChangesAsync();
        }
    }
}
