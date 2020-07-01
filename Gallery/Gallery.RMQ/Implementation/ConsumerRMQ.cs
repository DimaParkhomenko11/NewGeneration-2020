using System;
using System.Text;
using System.Threading;
using Gallery.MQ.Abstraction;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace Gallery.RMQ.Implementation
{
    public class ConsumerRMQ : ConsumerMQ
    {
        private readonly string _connectionString;
        private readonly TimeSpan _delay = TimeSpan.FromSeconds(3);

        public ConsumerRMQ(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public override T ReadMessage<T>(string queueName)
        {
            T messageReturn;
            var factory = new ConnectionFactory
            {
                Uri = new Uri(_connectionString)
            };
            using (var connection = factory.CreateConnection())
            using (var model = connection.CreateModel())
            {
                while (true)
                {
                    var msgCount = model.MessageCount(queueName);
                    if (msgCount > 0)
                    {
                        var getResult = model.BasicGet(queueName, true);
                        var body = getResult.Body.ToArray();
                        messageReturn = Deserialize<T>(DeserializeToString(body));
                        break;
                    }
                    Thread.Sleep(_delay);
                }
            }
            return messageReturn;
        }

        private static string DeserializeToString(byte[] obj) =>
            Encoding.UTF8.GetString(obj);

        private static T Deserialize<T>(string obj) =>
            JsonConvert.DeserializeObject<T>(obj);
    }
}
