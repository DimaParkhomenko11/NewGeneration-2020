using System;
using System.Configuration;
using Gallery.MQ.Interfaces;

namespace Gallery.MQ.InterfaceImplementation
{
    public class ParserMSMQ : IParserMQ
    {
        public string[] ParserMQ()
        {
            var queues = ConfigurationManager.AppSettings["MessageQueuingPath"] ?? throw new ArgumentException();
            return queues.Split(',');
        }
    }
}
