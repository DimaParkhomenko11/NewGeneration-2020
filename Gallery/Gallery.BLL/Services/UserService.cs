using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gallery.BLL.Contract;
using Gallery.BLL.Interfaces;
using Gallery.DAL.Interfaces;
using Gallery.DAL.Models;

namespace Gallery.BLL.Services
{
    public class UserService : IUsersService
    {
        private readonly IRepository _repository;

        public UserService(IRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<bool> IsUserExistAsync(string username, string plainPassword)
        {
            return await _repository.IsUserExistAsync(username, plainPassword);
        }

        public async Task<UserDto> FindUserAsync(string username, string plainPassword)
        {
            throw new NotImplementedException();
        }

        public async Task AddUserAsync(UserDto dto)
        {
            await _repository.AddUserToDatabaseAsync(dto.UserEmail, dto.PlainPassword);
        }

        public int GetIdUsers(string username)
        {
            return _repository.GetIdUsers(username);
        }

        public string GetNameUsers(int id)
        {
            return _repository.GetNameUsers(id);
        }

        public async Task AddAttemptAsync(string email, string ipAddress, bool isSuccess)
        {
            await _repository.AddAttemptToDatabaseAsync(email, ipAddress, isSuccess);
        }

    }
}
