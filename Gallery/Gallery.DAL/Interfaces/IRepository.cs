using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gallery.DAL.Interfaces
{
    public interface IRepository
    {
        Task<bool> IsUserExistAsync(string username, string plainPassword);

        Task AddUserToDatabaseAsync(string username, string plainPassword, int roleId);

        int GetIdUsers(string username);

        string GetNameUsers(int id);

    }
}
