using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gallery.BLL.Contract
{
    public class AddUserDto
    {
        public AddUserDto(string username, string plainPassword)
        {
            Username = username;
            PlainPassword = plainPassword;
        }

        public string Username { get;  }

        public string PlainPassword { get;  }
    }
}
