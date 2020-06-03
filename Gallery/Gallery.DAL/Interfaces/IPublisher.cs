using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gallery.DAL.Interfaces
{
    public interface IPublisher
    {
        void PublishMessage(object file, string queuePath, string queueName);
    }
}
