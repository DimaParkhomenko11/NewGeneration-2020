using System.Net;
using System.Threading.Tasks;
using Gallery.DAL.Models;

namespace Gallery.DAL.Interfaces
{
    public interface IRepository
    {
        Task<bool> IsUserExistAsync(string username, string plainPassword);

        Task AddUserToDatabaseAsync(string username, string plainPassword);

        int GetIdUsers(string username);

        string GetNameUsers(int id);

        Task AddAttemptToDatabaseAsync(string email, string ipAddress, bool isSuccess);
    }
}
