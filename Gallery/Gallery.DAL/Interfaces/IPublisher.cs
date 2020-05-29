using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gallery.DAL.Interfaces
{
    public interface IPublisher
    {
        void PublishMessage(byte[] fileBytes, string queuePath, string queueName);
    }
}
