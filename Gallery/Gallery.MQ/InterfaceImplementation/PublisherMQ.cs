using Gallery.MQ.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gallery.MQ.InterfaceImplementation
{
    public class PublisherMQ : IPublisherMQ
    {
        public void PublishMessage(object file, string queuePath, string queueName)
        {
            using (var queue = MessageQueue.Create(queuePath))
            {
                queue.Send(file, queueName);
            }
        }
    }
}
