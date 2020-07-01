using System;
using System.Text;
using Gallery.MQ.Abstraction;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace Gallery.RMQ.Implementation
{
    public class PublisherRMQ : PublisherMQ
    {
        private readonly string _connectionString;

        public PublisherRMQ(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public override void PublishMessage<T>(T message, string queueName)
        {
            var factory = new ConnectionFactory()
            {
                Uri = new Uri(_connectionString)
            };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                var body = SerializeToBytes(SerializeToJson(message));
                channel.BasicPublish(exchange: "",
                    routingKey: queueName,
                    basicProperties: null,
                    body: body);
            }
        }

        private static byte[] SerializeToBytes(string obj) =>
            Encoding.UTF8.GetBytes(obj);

        private static string SerializeToJson<T>(T obj) =>
            JsonConvert.SerializeObject(obj);

       
    }
}
