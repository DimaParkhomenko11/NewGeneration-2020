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

        public async Task<bool> IsUserExistAsync(UserDto userDto)
        {
            return await _repository.IsUserExistAsync(userDto.UserEmail, userDto.UserPassword);
        }

        public async Task<bool> IsUserExistByEmailAsync(UserDto userDto)
        {
            return await _repository.IsUserExistByEmailAsync(userDto.UserEmail);
        }


        public async Task<UserDto> FindUserAsync(UserDto userDto)
        {
            var user = await _repository.FindUserAsync(userDto.UserEmail, userDto.UserPassword);
            return new UserDto
            {
                UserId = user.Id,
                UserEmail = user.Email,
                UserPassword = user.Password,
                UserRole = user.Roles.ToList()
            };
        }

        public async Task AddUserAsync(UserDto userDto)
        {
            await _repository.AddUserToDatabaseAsync(userDto.UserEmail, userDto.UserPassword);
        }

        public int GetIdUsers(string username)
        {
            return _repository.GetIdUsers(username);
        }

        public string GetNameUsers(int id)
        {
            return _repository.GetNameUsers(id);
        }

        public async Task AddAttemptAsync(AttemptDTO attemptDto)
        {
            await _repository.AddAttemptToDatabaseAsync(attemptDto.Email, attemptDto.IpAddress, attemptDto.IsSuccess);
        }

    }
}
