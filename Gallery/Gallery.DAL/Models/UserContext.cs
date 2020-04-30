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
        public DbSet<MediaType> MediaTypes { get; set; }
    }

    public class UserDbInitializer : DropCreateDatabaseAlways<UserContext>
    {
        protected override void Seed(UserContext db)
        {
            User user1 = new User { Id = 1, Email = "dima@ukr.net", Password = "123"};
            User user2 = new User { Id = 1, Email = "admin@ukr.net", Password = "123" };

            db.Users.AddRange(new List<User> { user1, user2});
            db.SaveChanges();

            Role role1 = new Role { Name = "admin" };
            Role role2 = new Role { Name = "user" };
            Role role3 = new Role { Name = "user_premium" };
            role1.Users.Add(user2);
            role2.Users.Add(user1);
            role3.Users.Add(user1);
            db.Roles.AddRange(new List<Role>{role1, role2, role3});
            db.SaveChanges();

            Media pl1 = new Media {Name = "Media1", User = user1 };
            Media pl2 = new Media { Name = "Media2", User = user2 }; 
            Media pl3 = new Media { Name = "Media3", User = user2 };
            db.Medias.AddRange(new List<Media> { pl1, pl2, pl3 });
            db.SaveChanges();

            MediaType mt1 = new MediaType {Image = "Image1", Video = "Video1", Sound = "Sound1", Media = pl1 };
            MediaType mt2 = new MediaType { Image = "Image2", Video = "Video2", Sound = "Sound2", Media = pl1 };
            MediaType mt3 = new MediaType { Image = "Image3", Video = "Video3", Sound = "Sound3", Media = pl2 };
            db.MediaTypes.AddRange(new List<MediaType> { mt1, mt2, mt3 });
            db.SaveChanges();

        }
    }
}
