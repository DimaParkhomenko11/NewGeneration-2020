using Gallery.MQ.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Gallery.MQ.InterfaceImplementation
{
    public class PublisherMQ : IPublisherMQ
    {
        public void PublishMessage<T>(T message, string queue, string queueName)
        {
            var messageQueue = new MessageQueue(queue);
            messageQueue.Formatter = new BinaryMessageFormatter();
            var bytes = SerializeToBytes(SerializeToJson(message));
            messageQueue.Send(bytes, queueName);
        }

        private byte[] SerializeToBytes(string obj) =>
            Encoding.UTF8.GetBytes(obj);

        private static string SerializeToJson<T>(T obj) =>
            JsonConvert.SerializeObject(obj);
    }
}