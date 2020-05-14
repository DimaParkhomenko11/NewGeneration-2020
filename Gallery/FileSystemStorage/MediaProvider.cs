using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace FileSystemStorage
{
    public class MediaProvider : IMediaProvider
    {
        public bool Upload(byte[] dateBytes, string path)
        {
            File.WriteAllBytes(path, dateBytes);
            return File.Exists(path);
        }

        public byte[] Read(string path)
        {
            return File.ReadAllBytes(path);
        }

        public bool Delete(string path)
        {
            File.Delete(path);
            return File.Exists(path);
        }
    }
}
