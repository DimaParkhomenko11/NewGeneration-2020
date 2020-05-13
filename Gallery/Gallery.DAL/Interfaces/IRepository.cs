﻿using System.Net;
using System.Threading.Tasks;
using Gallery.DAL.Models;

namespace Gallery.DAL.Interfaces
{
    public interface IRepository
    {
        Task<bool> IsUserExistAsync(string userEmail, string plainPassword);

        Task AddUserToDatabaseAsync(string userEmail, string plainPassword);

        int GetIdUsers(string userEmail);

        string GetNameUsers(int id);

        Task AddAttemptToDatabaseAsync(string email, string ipAddress, bool isSuccess);
    }
}
