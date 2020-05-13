using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gallery.BLL.Contract
{
    public class UserDto
    {
        public UserDto(string username, string plainPassword)
        {
            UserEmail = username;
            PlainPassword = plainPassword;
        }

        public int UserId { get; protected set; }
        public string UserEmail { get; protected set; }
        public string PlainPassword { get; protected set; }
        public string UserRole { get; protected set; }
    }
}
