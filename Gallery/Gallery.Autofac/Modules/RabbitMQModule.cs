using Autofac;
using Gallery.Configurations.Management;
using Gallery.MQ.Abstraction;
using Gallery.RMQ;
using Gallery.RMQ.Implementation;

namespace Gallery.Autofac.Modules
{
    public class RabbitMQModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var rabbitMqConnectionString = ConfigurationManagement.RabbitMqConnectionString();
            builder.Register(ctx => new PublisherRMQ(rabbitMqConnectionString))
                .As<PublisherMQ>();

        }
    }
}
