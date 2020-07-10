
using System;

namespace Gallery.MQ.Abstraction
{
    public abstract class ConsumerMQ
    {
       public abstract void ReadMessage<T>(string queue, Action<T> action);
    }
}
