using System;
using System.Collections.Generic;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Gallery.DAL.Models
{
    public class UserContext : DbContext
    {
        public UserContext(string connectionString) : base(connectionString)
        { }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Media> Medias { get; set; }
    }

    public class UserDbInitializer : DropCreateDatabaseAlways<UserContext>
    {
        protected override void Seed(UserContext db)
        {
            /*User user1 = new User { Id = 1, Email = "dima@ukr.net", Password = "123"};
            User user2 = new User { Id = 1, Email = "admin@ukr.net", Password = "123" };

            db.Users.AddRange(new List<User> { user1, user2});
            db.SaveChanges();

            Role role1 = new Role { Name = "admin" };
            Role role2 = new Role { Name = "user" };
            Role role3 = new Role { Name = "user_premium" };
            role1.Users.Add(user2);
            role2.Users.Add(user1);
            role3.Users.Add(user1);
            db.Roles.Add(role1);
            db.Roles.Add(role2);
            db.Roles.Add(role3);
            db.SaveChanges();*/

            User user = new User {Id = 1, Email = "dima@ukr.net", Password = "123"};
            
            db.Users.Add(user);
            db.SaveChanges();

            Media pl1 = new Media {Name = "Media1", User = user };
            Media pl2 = new Media { Name = "Media2", User = user }; 
            Media pl3 = new Media { Name = "Media3", User = user };
            db.Medias.AddRange(new List<Media> { pl1, pl2, pl3 });
            db.SaveChanges();

        }
    }
}
