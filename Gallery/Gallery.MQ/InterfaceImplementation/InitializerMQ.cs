using System.Messaging;
using Gallery.MQ.Interfaces;
using RabbitMQ.Client;

namespace Gallery.MQ.InterfaceImplementation
{
    public class InitializerMQ : IInitializerMQ
    {
        public void Initializer(string[] queues)
        {
            foreach (var queuePath in queues)
            {
                if (!MessageQueue.Exists(queuePath))
                {
                    MessageQueue.Create(queuePath);
                }
            }
        }
        
    }
}
