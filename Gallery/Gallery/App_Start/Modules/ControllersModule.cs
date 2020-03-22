using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autofac;
using Gallery.BLL.Interfaces;
using Gallery.BLL.Services;
using Gallery.DAL.InterfaceImplementation;
using Gallery.DAL.Interfaces;
using Gallery.DAL.Models;

namespace Gallery.App_Start.Modules
{
    public class ControllersModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UserContext>()
                .AsSelf();
            builder.RegisterType<UsersRepository>()
                .As<IRepository>();
            builder.RegisterType<UserService>()
                .As<IUsersService>();

            builder.RegisterType<AuthenticationService>()
                .As<IAuthenticationService>();
        }
    }
}