using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Gallery.BLL.Interfaces;
using Gallery.BLL.Services;
using Gallery.DAL.InterfaceImplementation;
using Gallery.DAL.Interfaces;
using Gallery.DAL.Models;

namespace Gallery.App_Start
{
    public class DIConfig
    {
        public static void Configure(HttpConfiguration configuration)
        {
            var builder = new ContainerBuilder();

            
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            builder.RegisterType<UserContext>()
                .AsSelf();
            builder.RegisterType<UsersRepository>()
                .As<IRepository>();
            builder.RegisterType<UserService>()
                .As<IUsersService>();

            builder.RegisterType<AuthenticationService>()
                .As<IAuthenticationService>();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}