using System.Collections.Generic;
using Gallery.DAL.Models;

namespace Gallery.BLL.Contract
{
    public class UserDto
    {
        public int UserId { get; set; }
        public string UserEmail { get; set; }
        public string UserPassword { get; set; }
        public List<Role> UserRole { get; set; }
    }
}
