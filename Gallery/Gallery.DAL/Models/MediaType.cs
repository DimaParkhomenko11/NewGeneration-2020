using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gallery.DAL.Models
{
    public class MediaType
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string Video { get; set; }
        public string Sound { get; set; }

        public int? MediaId { get; set; }
        public Media Media { get; set; }
    }
}
