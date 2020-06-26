using Autofac;
using FileSystemStorage;
using Gallery.BLL.Interfaces;
using Gallery.BLL.Services;
using Gallery.Configurations.Management;
using Gallery.DAL.InterfaceImplementation;
using Gallery.DAL.Interfaces;
using Gallery.DAL.Models;
using Gallery.MQ.InterfaceImplementation;
using Gallery.MQ.Interfaces;
using Gallery.MQ.RabbitMQ.Implementation;
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

            var rabbitMqConnectionString = ConfigurationManagement.RabbitMqConnectionString();
            builder.Register(ctx => new PublisherRMQ(rabbitMqConnectionString)).As<IPublisherMQ>();

            /*builder.RegisterType<PublisherMQ>()
                .As<IPublisherMQ>();*/

            builder.RegisterType<NamingService>()
                .As<INamingService>();


        }
    }
}