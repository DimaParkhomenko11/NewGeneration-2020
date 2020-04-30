using System.Collections.Generic;
using Gallery.DAL.Models;

namespace Gallery.DAL
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public ICollection<Role> Roles { get; set; }
        public ICollection<Media> Medias { get; set; }

        public User()
        {
            Roles = new List<Role>();
            Medias = new List<Media>();
        }

    }
}