using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Gallery.MQ.Interfaces
{
    public interface IConsumerMQ
    {
        Message ReadMessage(string path);
    }
}
