using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gallery.DAL.Interfaces;
using Gallery.DAL.Models;

namespace Gallery.DAL.InterfaceImplementation
{
    public class MediaRepository : IMediaRepository
    {
        private readonly DbContext dbContext;

        public MediaRepository(DbContext context)
        {
            dbContext = context ?? throw new ArgumentNullException(nameof(context));
        }

    }
}
