namespace Gallery.MQ.Abstraction
{
    public abstract class PublisherMQ
    {
        public abstract void PublishMessage<T>(T message, string queueName);
    }
}
