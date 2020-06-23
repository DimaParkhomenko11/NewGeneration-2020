﻿using System;
using System.Configuration;
using Gallery.MQ.Interfaces;

namespace Gallery.MQ.InterfaceImplementation
{
    public class ParserMQ : IParserMQ
    {
        public string[] Parser()
        {
            var queues = ConfigurationManager.AppSettings["MessageQueuingPath"] ?? throw new ArgumentException();
            return queues.Split(',');
        }
    }
}
