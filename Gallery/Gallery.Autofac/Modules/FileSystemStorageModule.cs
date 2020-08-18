using System.IO.Abstractions;
using Autofac;
using FileSystemStorage.Implementation;
using FileSystemStorage.Interfaces;

namespace Gallery.Autofac.Modules
{
    public class FileSystemStorageModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(f => new FileSystem().File)
                .As<IFile>();
            builder.RegisterType<MediaProvider>()
                .As<IMediaProvider>();
        }
    }
}
