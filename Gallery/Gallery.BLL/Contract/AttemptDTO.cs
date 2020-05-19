using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gallery.BLL.Contract
{
    public class AttemptDTO
    {
        public string Email { get; set; }
        public string IpAddress { get; set; }
        public bool IsSuccess { get; set; }
    }
}
