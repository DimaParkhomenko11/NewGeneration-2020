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

        Task AddUserToDatabase(string username, string plainPassword);

        int GetIdUsers(string username);
    }
}
