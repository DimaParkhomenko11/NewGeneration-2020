using System;
using System.Messaging;
using Gallery.DAL.Interfaces;

namespace Gallery.DAL.InterfaceImplementation
{
    public class Publisher: IPublisher
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
