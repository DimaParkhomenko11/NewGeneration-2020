using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gallery.DAL.Models
{
    public class Media
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PathToMedia { get; set; }
        public bool isDeleted { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
        public int MediaTypeId { get; set; }
        public MediaType MediaType { get; set; }

    }
}
