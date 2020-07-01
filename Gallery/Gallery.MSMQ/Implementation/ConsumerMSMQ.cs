﻿using System.Messaging;
using System.Text;
using Gallery.MQ.Abstraction;
using Newtonsoft.Json;

namespace Gallery.MSMQ.Implementation
{
    public class ConsumerMSMQ : ConsumerMQ
    {
        public override T ReadMessage<T>(string queueName)
        {
            var queue = new MessageQueue(queueName);
            queue.Formatter = new BinaryMessageFormatter();
            var messageReceive = queue.Receive();
            var message = Deserialize<T>(DeserializeToString((byte[])messageReceive.Body));
            return message;
        }

        private string DeserializeToString(byte[] obj) =>
            Encoding.UTF8.GetString(obj);

        private static T Deserialize<T>(string obj) =>
            JsonConvert.DeserializeObject<T>(obj);
    }
}