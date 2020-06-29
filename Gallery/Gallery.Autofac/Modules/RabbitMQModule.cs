using Autofac;
using Gallery.Configurations.Management;
using Gallery.MQ.Interfaces;
using Gallery.MQ.RabbitMQ.Implementation;

namespace Gallery.Autofac.Modules
{
    public class RabbitMQModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var rabbitMqConnectionString = ConfigurationManagement.RabbitMqConnectionString();
            builder.Register(ctx => new PublisherRMQ(rabbitMqConnectionString))
                .As<IPublisherMQ>();

        }
    }
}
