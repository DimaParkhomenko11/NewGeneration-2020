using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gallery.BLL.Interfaces
{
    public interface INamingService
    {

        string NameCleaner(string fileName);
    }
}
