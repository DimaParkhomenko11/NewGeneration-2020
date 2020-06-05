using Autofac;
using FileSystemStorage;
using Gallery.BLL.Interfaces;
using Gallery.BLL.Services;
using Gallery.ConfigManagement;
using Gallery.DAL.InterfaceImplementation;
using Gallery.DAL.Interfaces;
using Gallery.DAL.Models;
using Gallery.MQ.InterfaceImplementation;
using Gallery.MQ.Interfaces;
using Gallery.Services;

namespace Gallery.Modules
{
    public class ControllersModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //AccountController
            var connectionString = ConfigurationManagement.DBConnectionString();
            builder.Register(ctx => new SqlDbContext(connectionString)).AsSelf();

            builder.RegisterType<UsersRepository>()
                .As<IRepository>();
            builder.RegisterType<UserService>()
                .As<IUsersService>();

            builder.RegisterType<AuthenticationService>()
                .As<IAuthenticationService>();

            //HomeController
            builder.RegisterType<ImageServices>()
                .As<IImagesService>();

            builder.RegisterType<HashService>()
                .As<IHashService>();

            builder.RegisterType<ConfigurationManagement>()
                .AsSelf();

            builder.RegisterType<MediaProvider>()
                .As<IMediaProvider>();

            builder.RegisterType<MediaRepository>()
                .As<IMediaRepository>();

            builder.RegisterType<PublisherMQ>()
                .As<IPublisherMQ>();

            builder.RegisterType<NamingService>()
                .As<INamingService>();


        }
    }
}