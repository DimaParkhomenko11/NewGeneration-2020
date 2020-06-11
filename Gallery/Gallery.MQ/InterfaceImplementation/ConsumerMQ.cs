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
        public void ReadMessage(string path)
        {
            var queue = new MessageQueue(path);
            queue.Formatter = new BinaryMessageFormatter();
            Message message = queue.Receive();
            Console.WriteLine(message.Body);
            Console.WriteLine((Byte[])message.Body);
        }

    }
}
