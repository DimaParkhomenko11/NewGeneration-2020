using System;
using System.Collections.Generic;
using System.Data.Entity;
using Gallery.DAL.Models.ModelConfiguration;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Gallery.DAL.Models
{
    public class SqlDbContext : System.Data.Entity.DbContext
    {
        public SqlDbContext()
        {
        }

        public SqlDbContext(string connectionString) : base(connectionString)
        { }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Media> Media { get; set; }
        public DbSet<MediaType> MediaTypes { get; set; }
        public DbSet<Attempt> Attempts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserConfiguration());
            modelBuilder.Configurations.Add(new RoleConfiguration());
            modelBuilder.Configurations.Add(new MediaConfiguration());
            modelBuilder.Configurations.Add(new MediaTypeConfiguration());
            modelBuilder.Configurations.Add(new AttemptConfiguration());


            base.OnModelCreating(modelBuilder);
        }

        
    }
    
}
