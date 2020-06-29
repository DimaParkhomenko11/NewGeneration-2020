using Autofac;
using Gallery.BLL.Interfaces;
using Gallery.BLL.Services;

namespace Gallery.Autofac.Modules
{
    public class ServicesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UserService>()
                .As<IUsersService>();
            builder.RegisterType<ImageServices>()
                .As<IImagesService>();
            builder.RegisterType<HashService>()
                .As<IHashService>();
            builder.RegisterType<NamingService>()
                .As<INamingService>();
        }
    }
}
