using System.Messaging;
using Gallery.MQ.Abstraction;

namespace Gallery.MSMQ.Implementation
{
    public class InitializerMSMQ : InitializerMQ
    {
        public override void Initializer(string[] queues)
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
