using System.Messaging;
using System.Text;
using Gallery.MQ.Abstraction;
using Newtonsoft.Json;

namespace Gallery.MSMQ.Implementation
{
    public class PublisherMSMQ : PublisherMQ
    {
        public override void PublishMessage<T>(T message, string queueName)
        {
            var messageQueue = new MessageQueue(queueName);
            messageQueue.Formatter = new BinaryMessageFormatter();
            var bytes = SerializeToBytes(SerializeToJson(message));
            messageQueue.Send(bytes);
        }

        private static byte[] SerializeToBytes(string obj) =>
            Encoding.UTF8.GetBytes(obj);

        private static string SerializeToJson<T>(T obj) =>
            JsonConvert.SerializeObject(obj);
    }
}