using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Gallery.DAL.Models;

namespace Gallery.DAL.DatabaseInteraction
{
    public class DatabaseInteraction
    {
        public User AuthorizationInteraction(LoginModel model)
        {
            User user = null;
            using (UserContext database = new UserContext())
            {
                user = database.Users.FirstOrDefault(u => u.Email == model.Name && u.Password == model.Password);
            }
            return user;
        }

        public User RegistrationInteraction(RegisterModel model)
        {
            User user = null;
            using (UserContext database = new UserContext())
            {
                user = database.Users.FirstOrDefault(u => u.Email == model.Name);
            }
            return user;
        }

        public User CreateNewUser(User user, RegisterModel model)
        {
            using (UserContext database = new UserContext())
            {
                database.Users.Add(new User { Email = model.Name, Password = model.Password });
                database.SaveChanges();
                user = database.Users.Where(u => u.Email == model.Name && u.Password == model.Password)
                    .FirstOrDefault();
            }
            return user;
        }


    }
}
