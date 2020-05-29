using System;
using System.Messaging;
using Gallery.DAL.Interfaces;

namespace Gallery.DAL.InterfaceImplementation
{
    public class Publisher: IPublisher
    {
        public void PublishMessage(byte[] fileBytes, string queuePath, string queueName)
        {
            using (var queue = MessageQueue.Create(queuePath))
            {
                queue.Send(fileBytes, queueName);
            }
        }
    }
}
