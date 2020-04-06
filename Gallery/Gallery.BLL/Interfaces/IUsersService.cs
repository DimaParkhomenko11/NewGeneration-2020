using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gallery.BLL.Contract;

namespace Gallery.BLL.Interfaces
{
    public interface IUsersService
    {
        Task<bool> IsUserExistAsync(string username, string plainPassword);

        Task<UserDto> FindUserAsync(string username, string plainPassword);

        Task AddUserAsync(AddUserDto dto);

        int GetIdUsers(string username);

        string GetNameUsers(int id);

       
    }
}
