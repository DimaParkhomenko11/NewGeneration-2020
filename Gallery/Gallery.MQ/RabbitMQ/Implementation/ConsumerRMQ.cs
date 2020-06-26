using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Gallery.MQ.Interfaces;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Gallery.MQ.RabbitMQ.Implementation
{
    public class ConsumerRMQ : IConsumerMQ
    {
        private readonly Uri _uri;
        public ConsumerRMQ(string connectionString)
        {
            _uri = new Uri(connectionString);
        }
        static object locker = new object();
        public T ReadMessage<T>(string queueName)
        {
            T mesegeReturn = default(T);
            lock (locker)
            {
                var factory = new ConnectionFactory()
                {
                    Uri = _uri
                };
                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body.ToArray();
                        var message = Deserialize<T>(DeserializeToString(body));
                        mesegeReturn = message;
                    };
                    channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
                    Thread.Sleep(1000);
                }
            }
            return mesegeReturn;
        }

        private static string DeserializeToString(byte[] obj) =>
            Encoding.UTF8.GetString(obj);

        private static T Deserialize<T>(string obj) =>
            JsonConvert.DeserializeObject<T>(obj);
    }
}
