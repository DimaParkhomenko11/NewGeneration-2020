using System.Collections.Generic;

namespace Gallery.MQ.Abstraction
{
    public abstract class InitializerMQ
    {
        public abstract void Initializer(Dictionary<string, string> queues);
    }
}
