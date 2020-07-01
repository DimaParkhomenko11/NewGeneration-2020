using System;
using System.Configuration;
using Gallery.MQ.Abstraction;

namespace Gallery.MSMQ.Implementation
{
    public class ParserMSMQ : ParserMQ
    {
        public override string[] ParserMq()
        {
            var queues = ConfigurationManager.AppSettings["MessageQueuingPath"] ?? throw new ArgumentException();
            return queues.Split(',');
        }
    }
}
