using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gallery.DAL.Models
{
    public enum Type
    {
        Video,
        Sound,
        Image
    }
    public class MediaType
    {
        public int Id { get; set; }
        public Type Type { get; set; }

        public ICollection<Media> Medias { get; set; }

        public MediaType()
        {
            Medias = new List<Media>();
        }

    }
}
