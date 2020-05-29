using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gallery.DAL.Models
{
    public class TemporaryMedia
    {
        public int Id { get; set; }
        public string UniqueIdentName { get; set; }
        public bool InDuringLoading { get; set; }
        public bool IsSuccess { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }
        
    }
}
