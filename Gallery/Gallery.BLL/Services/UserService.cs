using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gallery.BLL.Contract;
using Gallery.BLL.Interfaces;
using Gallery.DAL.Interfaces;

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

        public async Task AddUserAsync(AddUserDto dto)
        {
            await _repository.AddUserToDatabaseAsync(dto.Username, dto.PlainPassword);
        }

        public int GetIdUsers(string username)
        {
            return _repository.GetIdUsers(username);
        }

        public async Task<bool> IsConnectionAvailableAsync()
        {
            return await _repository.IsConnectionAvailableAsync();
        }
    }
}
