using System;
using System.Collections.Generic;
using Azure.Storage.Queues;
using Gallery.MQ.Abstraction;

namespace Gallery.ASQ.Implementation
{
    public class InitializerASQ : InitializerMQ
    {
        private readonly string _connectionString;

        public InitializerASQ(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }
        public override void Initializer(Dictionary<string, string> queues)
        {
            foreach (var queueName in queues)
            {
                // Instantiate a QueueClient which will be used to create and manipulate the queue
                QueueClient queueClient = new QueueClient(_connectionString, queueName.Value);
                // Create the queue
                queueClient.CreateIfNotExists();
            }
        }

    }
}
