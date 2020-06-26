using System;
using System.Text;
using Gallery.MQ.Interfaces;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace Gallery.MQ.RabbitMQ.Implementation
{
    public class PublisherRMQ : IPublisherMQ
    {
        private readonly Uri _uri;

        public PublisherRMQ(string connectionString)
        {
            _uri = new Uri(connectionString);
        }

        public void PublishMessage<T>(T message, string queueName)
        {
            var factory = new ConnectionFactory()
            {
                Uri = _uri
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
