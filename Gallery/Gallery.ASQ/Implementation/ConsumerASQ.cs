using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;
using Gallery.MQ.Abstraction;
using Newtonsoft.Json;

namespace Gallery.ASQ.Implementation
{
    public class ConsumerASQ : ConsumerMQ
    {
        private readonly string _connectionString;
        private readonly TimeSpan _delay = TimeSpan.FromSeconds(3);

        public ConsumerASQ(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public override void ReadMessage<T>(string queue, Action<T> action)
        {
            while (true)
            {
                QueueClient queueClient = new QueueClient(_connectionString, queue);
                QueueMessage[] retrievedMessage = queueClient.ReceiveMessages();
                if (retrievedMessage.Length > 0)
                {
                    var message = Deserialize<T>(retrievedMessage[0].MessageText);
                    queueClient.DeleteMessage(retrievedMessage[0].MessageId, retrievedMessage[0].PopReceipt);
                    action(message);
                    break;
                }
                Thread.Sleep(_delay);
            }
        }

        private static T Deserialize<T>(string obj) =>
            JsonConvert.DeserializeObject<T>(obj);
    }
}
