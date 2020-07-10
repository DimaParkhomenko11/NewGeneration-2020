using Autofac;
using Gallery.ASQ.Implementation;
using Gallery.Configurations.Management;
using Gallery.MQ.Abstraction;

namespace Gallery.Autofac.Modules
{
    public class AzureSQModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var azureConnectionString = ConfigurationManagement.AzureMqConnectionString();
            builder.Register(ctx => new PublisherASQ(azureConnectionString))
                .As<PublisherMQ>();
        }
    }
}
