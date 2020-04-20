using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Gallery.DAL.Models
{
    public class UserContext : DbContext
    {

        public UserContext() : base("DefaultConnection")
        { }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }


    }

    public class UserDbInitializer : DropCreateDatabaseAlways<UserContext>
    {
        protected override void Seed(UserContext db)
        {
            db.Roles.Add(new Role { Id = 1, Name = "admin" });
            db.Roles.Add(new Role { Id = 2, Name = "user" });
            db.Users.Add(new User
            {
                Email = "admine@ukr.net",
                Password = "12345",
                RoleId = 1
            });
            db.Users.Add(new User
            {
                Email = "tom@ukr.net",
                Password = "321",
                RoleId = 2
            });
            base.Seed(db);
        }
    }
}
