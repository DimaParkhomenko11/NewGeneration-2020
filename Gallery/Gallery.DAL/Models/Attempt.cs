using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gallery.DAL.Models
{
    public class Attempt
    {
        public int Id { get; set; }
        public DateTime TimeStamp { get; set; }
        public bool Success { get; set; }
        public string IpAddress { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
