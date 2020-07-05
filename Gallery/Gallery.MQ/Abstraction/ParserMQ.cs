using System;
using System.Collections.Generic;
using System.Configuration;

namespace Gallery.MQ.Abstraction
{
    public class ParserMQ
    {
        public Dictionary<string, string> ParserMq()
        {
            var queueUploadImage = ConfigurationManager.AppSettings["queue:upload-image"] ?? throw new ArgumentException();
            var queueUploadMp3 = ConfigurationManager.AppSettings["queue:upload-mp3"] ?? throw new ArgumentException();
            var queueUploadMp4 = ConfigurationManager.AppSettings["queue:upload-mp4"] ?? throw new ArgumentException();

            Dictionary<string, string> queueDictionary = new Dictionary<string, string>
            {
                { "queue:upload-image", queueUploadImage },
                { "queue:upload-mp3", queueUploadMp3 },
                { "queue:upload-mp4", queueUploadMp4 }
            };
            return queueDictionary;

        }
    }
}
