using System.Collections.Generic;
using Gallery.DAL.Models;

namespace Gallery.DAL
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public virtual ICollection<Role> Roles { get; set; } = new List<Role>();
        public virtual ICollection<Media> Media { get; set; } = new List<Media>();
        public virtual ICollection<Attempt> Attempts { get; set; } = new List<Attempt>();

    }
}