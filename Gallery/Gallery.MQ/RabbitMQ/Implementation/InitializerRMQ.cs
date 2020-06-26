using System;
using Gallery.MQ.Interfaces;
using RabbitMQ.Client;

namespace Gallery.MQ.RabbitMQ.Implementation
{
    public class InitializerRMQ : IInitializerMQ
    {
        private readonly Uri _uri;

        public InitializerRMQ(string connectionString)
        {
            _uri = new Uri(connectionString);
        }
        public void Initializer(string[] queues)
        {
            var factory = new ConnectionFactory()
            {
                Uri = _uri
            };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                foreach (var queueName in queues)
                {
                    channel.QueueDeclare(queue: queueName,
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);
                }
                
            }
        }
    }
}
