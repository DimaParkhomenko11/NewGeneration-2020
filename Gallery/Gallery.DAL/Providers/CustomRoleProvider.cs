using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using Gallery.DAL.Models;

namespace Gallery.DAL.Providers
{
    public class CustomRoleProvider : RoleProvider
    {
        public override string[] GetRolesForUser(string username)
        {
            string[] roles = new string[] { };
            UserContext db = new UserContext();

            // Получаем пользователя
            User user = db.Users.Include(u => u.Role).FirstOrDefault(u => u.Email == username);
            if (user != null && user.Role != null)
            {
                // получаем роль
                roles = new string[] { user.Role.Name };
            }
            return roles;
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            UserContext db = new UserContext();

            // Получаем пользователя
            User user = db.Users.Include(u => u.Role).FirstOrDefault(u => u.Email == username);

            if (user != null && user.Role != null && user.Role.Name == roleName)
                return true;
            else
                return false;

        }
    }
}
