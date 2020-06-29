using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gallery.MQ.Interfaces;

namespace Gallery.MQ.RabbitMQ.Implementation
{
    public class ParserRMQ : IParserMQ
    {
        public string[] ParserMQ()
        {
            var queues = ConfigurationManager.AppSettings["RabbitMessageQueuing"] ?? throw new ArgumentException();
            return queues.Split(',');
        }
    }
}
