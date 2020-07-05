using System.Collections.Generic;
using System.Messaging;
using Gallery.MQ.Abstraction;

namespace Gallery.MSMQ.Implementation
{
    public class InitializerMSMQ : InitializerMQ
    {
        public override void Initializer(Dictionary<string, string> queues)
        {
            foreach (var queueName in queues)
            {
                var path = @".\private$\" + queueName.Value;
                if (!MessageQueue.Exists(path))
                {
                    MessageQueue.Create(path);
                }
            }
        }
        
    }
}
