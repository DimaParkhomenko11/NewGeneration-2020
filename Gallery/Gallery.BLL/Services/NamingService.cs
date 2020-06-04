using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Gallery.BLL.Interfaces;

namespace Gallery.BLL.Services
{
    public class NamingService : INamingService
    {
        public string NameCleaner(string fileName)
        {
            try
            {
                return Regex.Replace(fileName, @"[^\w\.@-]", "",
                    RegexOptions.None, TimeSpan.FromSeconds(1.5));
            }
            catch (RegexMatchTimeoutException)
            {
                return String.Empty;
            }
        }
    }
}
