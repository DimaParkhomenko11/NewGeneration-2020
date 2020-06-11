using Gallery.MQ.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Gallery.MQ.InterfaceImplementation
{
    public class ConsumerMQ : IConsumerMQ
    {
        public Message ReadMessage(string path)
        {
            var queue = new MessageQueue(path);
            queue.Formatter = new XmlMessageFormatter(
                new Type[] { typeof(string), typeof(byte) });
            var message = queue.Receive();
            return message;
        }
    }
}
