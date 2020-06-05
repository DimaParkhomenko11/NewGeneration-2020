using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gallery.MQ.Interfaces
{
    public interface IPublisherMQ
    {
        void PublishMessage(object file, string queuePath, string queueName);
    }
}
