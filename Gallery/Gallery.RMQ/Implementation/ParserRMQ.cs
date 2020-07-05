using System;
using System.Configuration;
using Gallery.MQ.Abstraction;

namespace Gallery.RMQ.Implementation
{
    public class ParserRMQ : ParserMQ
    {
        public override string[] ParserMq()
        {
            var queues = ConfigurationManager.AppSettings["queue:upload-image"] ?? throw new ArgumentException();
            return queues.Split(',');
        }
    }
}
