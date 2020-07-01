using Autofac;
using Gallery.MQ.Abstraction;

namespace Gallery.Autofac.Modules
{
    public class MSMQModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<PublisherMQ>()
                .As<PublisherMQ>();
        }
    }
}
