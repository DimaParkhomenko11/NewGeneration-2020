using System.Web.Http;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Gallery.Autofac.Modules;
using Gallery.Modules;

namespace Gallery.App_Start
{
    public class DIConfig
    {
        public static void Configure(HttpConfiguration configuration)
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            builder.RegisterModule<ControllersModule>();

            builder.RegisterModule<FileSystemStorageModule>();

            builder.RegisterModule<RabbitMQModule>();

            builder.RegisterModule<RepositorysModule>();

            builder.RegisterModule<ServicesModule>();

            /*builder.RegisterModule<MSMQModule>();*/

            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}