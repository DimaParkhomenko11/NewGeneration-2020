
namespace Gallery.MQ.Abstraction
{
    public abstract class ConsumerMQ
    {
       public abstract T ReadMessage<T>(string queue);
    }
}
