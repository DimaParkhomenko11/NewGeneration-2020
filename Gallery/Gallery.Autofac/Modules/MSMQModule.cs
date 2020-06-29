using Autofac;
using Gallery.MQ.InterfaceImplementation;
using Gallery.MQ.Interfaces;

namespace Gallery.Autofac.Modules
{
    public class MSMQModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<PublisherMQ>()
                .As<IPublisherMQ>();
        }
    }
}
