using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Gallery.DAL.Interfaces;
using Gallery.DAL.Models;
using DbContext = Gallery.DAL.Models.DbContext;

namespace Gallery.DAL.InterfaceImplementation
{
    public class UsersRepository : IRepository
    {
        private readonly DbContext dbContext;

        public UsersRepository(DbContext context)
        {
            dbContext = context ?? throw new ArgumentNullException(nameof(context));

        }

        public async Task<bool> IsUserExistAsync(string userEmail, string plainPassword)
        {

            return await dbContext.Users.AnyAsync(u => u.Email == userEmail.Trim().ToLower() && u.Password == plainPassword.Trim());

        }

        public async Task AddUserToDatabaseAsync(string userEmail, string plainPassword)
        {
            User user = new User { Email = userEmail, Password = plainPassword };
            dbContext.Users.Add(user);
            dbContext.SaveChanges();

            Role role = dbContext.Roles.First(p => p.Name == "user");

            user.Roles.Add(role);
            dbContext.SaveChanges();

            await dbContext.SaveChangesAsync();
        }

        public int GetIdUsers(string userEmail)
        {
            return dbContext.Users.Where(u => u.Email == userEmail).Select(u => u.Id).FirstOrDefault();
        }

        public string GetNameUsers(int id)
        {
            return dbContext.Users.Where(u => u.Id == id).Select(u => u.Email).FirstOrDefault();
        }

        public async Task AddAttemptToDatabaseAsync(string email, string ipAddress, bool isSuccess)
        {
            var user = await dbContext.Users.FirstOrDefaultAsync(p => p.Email == email);
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            Attempt attempt2 = new Attempt { Id = 1, TimeStamp = DateTime.Now, Success = isSuccess, IpAddress = ipAddress, User = user };
            dbContext.Attempts.Add(attempt2);
            await dbContext.SaveChangesAsync();
        }

    }
}
