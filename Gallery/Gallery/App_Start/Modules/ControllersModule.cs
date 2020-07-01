using Autofac;
using FileSystemStorage;
using Gallery.BLL.Interfaces;
using Gallery.BLL.Services;
using Gallery.Configurations.Management;
using Gallery.Controllers;
using Gallery.DAL.InterfaceImplementation;
using Gallery.DAL.Interfaces;
using Gallery.DAL.Models;
using Gallery.MQ.Abstraction;
using Gallery.Services;

namespace Gallery.Modules
{
    public class ControllersModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AuthenticationService>()
                .As<IAuthenticationService>();
            builder.Register(h =>
                new HomeController(
                    h.Resolve<IImagesService>(),
                    h.Resolve<IHashService>(),
                    h.Resolve<IUsersService>(),
                    h.Resolve<INamingService>(),
                    h.Resolve<PublisherMQ>()))
                .InstancePerRequest();

            builder.Register(a =>
                new AccountController(
                    a.Resolve<IUsersService>(),
                    a.Resolve<IAuthenticationService>()))
                .InstancePerRequest();
            /*builder.RegisterType<ConfigurationManagement>()
                .AsSelf();*/
            
        }
    }
}