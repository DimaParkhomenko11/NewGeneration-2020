using System;
using System.Collections.Generic;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Gallery.DAL.Models
{
    public class UserContext : DbContext
    {
        public UserContext() 
        { }

        public UserContext(string connectionString) : base(connectionString)
        { }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Media> Media { get; set; }
        public DbSet<MediaType> MediaTypes { get; set; }
        public DbSet<Attempt> Attempts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Role>().ToTable("Roles");
            modelBuilder.Entity<Media>().ToTable("Media");
            modelBuilder.Entity<MediaType>().ToTable("MediaTypes");

            modelBuilder.Entity<User>().HasKey(p => p.Id);
            modelBuilder.Entity<Role>().HasKey(p => p.Id);
            modelBuilder.Entity<Media>().HasKey(p => p.Id);
            modelBuilder.Entity<MediaType>().HasKey(p => p.Id);

            modelBuilder.Entity<User>().Property(p => p.Id).IsRequired();
            modelBuilder.Entity<User>().Property(p => p.Email).IsRequired();
            modelBuilder.Entity<User>().Property(p => p.Password).IsRequired();

            modelBuilder.Entity<User>().Property(p => p.Email).HasMaxLength(40);
            modelBuilder.Entity<User>().Property(p => p.Password).HasMaxLength(50);
            modelBuilder.Entity<Role>().Property(p => p.Name).HasMaxLength(30);
            modelBuilder.Entity<MediaType>().Property(p => p.Type).HasMaxLength(25);
            modelBuilder.Entity<Media>().Property(p => p.PathToMedia).HasMaxLength(25);

            modelBuilder.Entity<User>().Property(p => p.Email).HasColumnType("varchar");
            modelBuilder.Entity<User>().Property(p => p.Password).HasColumnType("varchar");
            modelBuilder.Entity<Role>().Property(p => p.Name).HasColumnType("varchar");
            modelBuilder.Entity<MediaType>().Property(p => p.Type).HasColumnType("varchar");
            modelBuilder.Entity<Media>().Property(p => p.PathToMedia).HasColumnType("varchar");


            modelBuilder.Entity<User>()
                .HasMany(p => p.Roles)
                .WithMany(c => c.Users);


            modelBuilder.Entity<User>()
                .HasMany(p => p.Media)
                .WithRequired(p => p.User);
            modelBuilder.Entity<MediaType>()
                .HasMany(p => p.Media)
                .WithRequired(p => p.MediaType);
           
            base.OnModelCreating(modelBuilder);
        }
    }

    
}
