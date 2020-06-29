using Autofac;
using Gallery.Configurations.Management;
using Gallery.DAL.InterfaceImplementation;
using Gallery.DAL.Interfaces;
using Gallery.DAL.Models;

namespace Gallery.Autofac.Modules
{
    public class RepositorysModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var connectionString = ConfigurationManagement.DBConnectionString();
            builder.Register(ctx => new SqlDbContext(connectionString))
                .AsSelf();
            builder.RegisterType<MediaRepository>()
                .As<IMediaRepository>();
            builder.RegisterType<UsersRepository>()
                .As<IRepository>();
        }
    }
}
