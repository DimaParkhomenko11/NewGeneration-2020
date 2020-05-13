using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gallery.BLL.Contract;
using Gallery.DAL.Models;

namespace Gallery.BLL.Interfaces
{
    public interface IUsersService
    {
        Task<bool> IsUserExistAsync(string username, string plainPassword);

        Task<UserDto> FindUserAsync(string username, string plainPassword);

        Task AddUserAsync(UserDto dto);

        int GetIdUsers(string username);

        string GetNameUsers(int id);

        Task AddAttemptAsync(string email, string ipAddress, bool isSuccess);
    }
}
