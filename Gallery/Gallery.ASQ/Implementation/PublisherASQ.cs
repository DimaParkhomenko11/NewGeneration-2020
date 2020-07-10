using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Storage.Queues;
using Gallery.MQ.Abstraction;
using Newtonsoft.Json;

namespace Gallery.ASQ.Implementation
{
    public class PublisherASQ : PublisherMQ
    {
        private readonly string _connectionString;

        public PublisherASQ(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }
        public override void PublishMessage<T>(T message, string queueName)
        {
            QueueClient queueClient = new QueueClient(_connectionString, queueName);
            if (queueClient.Exists())
            { 
                // Send a message to the queue
                queueClient.SendMessage(SerializeToJson(message));
            }
        }

        private static string SerializeToJson<T>(T obj) =>
            JsonConvert.SerializeObject(obj);
    }
}
