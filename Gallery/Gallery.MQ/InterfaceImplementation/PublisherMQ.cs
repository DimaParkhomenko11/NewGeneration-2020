using Gallery.MQ.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Gallery.MQ.InterfaceImplementation
{
    public class PublisherMQ : IPublisherMQ
    {


        public void PublishMessage(object file, string queuePath, string queueName)
        {

            if (!MessageQueue.Exists(queuePath))
            {
                MessageQueue.Create(queuePath);
            }
            var queue = new MessageQueue(queuePath);
            var myMessage = new Message(file, new BinaryMessageFormatter());
            queue.Send(myMessage);

        }
    }
}
