using Autofac;
using FileSystemStorage.Implementation;
using FileSystemStorage.Interfaces;

namespace Gallery.Autofac.Modules
{
    public class FileSystemStorageModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MediaProvider>()
                .As<IMediaProvider>();
        }
    }
}
