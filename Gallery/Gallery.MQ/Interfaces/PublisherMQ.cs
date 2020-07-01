using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gallery.MQ.Interfaces
{
    public abstract class PublisherMQ
    {
        public abstract void PublishMessage<T>(T message, string queueName);
    }
}
