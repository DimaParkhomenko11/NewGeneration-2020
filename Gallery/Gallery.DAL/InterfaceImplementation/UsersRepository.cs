using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Gallery.DAL.Interfaces;
using Gallery.DAL.Models;

namespace Gallery.DAL.InterfaceImplementation
{
    public class UsersRepository : IRepository
    {
        private readonly SqlDbContext _sqlDbContext;

        public UsersRepository(SqlDbContext context)
        {
            _sqlDbContext = context ?? throw new ArgumentNullException(nameof(context));

        }

        public async Task<bool> IsUserExistAsync(string userEmail, string plainPassword)
        {

            return await _sqlDbContext.Users.AnyAsync(u => u.Email == userEmail && u.Password == plainPassword);

        }

        public async Task<bool> IsUserExistByEmailAsync(string userEmail)
        {
            return await _sqlDbContext.Users.AnyAsync(u => u.Email == userEmail);
        }

        public async Task AddUserToDatabaseAsync(string userEmail, string plainPassword)
        {
            User user = new User { Email = userEmail, Password = plainPassword };
            _sqlDbContext.Users.Add(user);
            _sqlDbContext.SaveChanges();

            Role role = _sqlDbContext.Roles.First(p => p.Name == "user");

            user.Roles.Add(role);
            _sqlDbContext.SaveChanges();

            await _sqlDbContext.SaveChangesAsync();
        }

        public async Task<User> FindUserAsync(string userEmail, string userPassword)
        {
            return await _sqlDbContext.Users.FirstOrDefaultAsync(u => u.Email == userEmail && u.Password == userPassword);
        }

        public int GetIdUsers(string userEmail)
        {
            return _sqlDbContext.Users.Where(u => u.Email == userEmail).Select(u => u.Id).FirstOrDefault();
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _sqlDbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task AddAttemptToDatabaseAsync(string email, string ipAddress, bool isSuccess)
        {
            var user = await _sqlDbContext.Users.FirstOrDefaultAsync(p => p.Email == email);
            Attempt attempt2 = new Attempt
            {
                TimeStamp = DateTime.Now, Success = isSuccess, IpAddress = ipAddress, User = user
            };
            _sqlDbContext.Attempts.Add(attempt2);
            await _sqlDbContext.SaveChangesAsync();
        }

    }
}
