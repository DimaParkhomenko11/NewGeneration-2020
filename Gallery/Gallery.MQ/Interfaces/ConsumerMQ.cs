using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Gallery.MQ.Interfaces
{
    public abstract class ConsumerMQ
    {
       public abstract T ReadMessage<T>(string queue);
    }
}
