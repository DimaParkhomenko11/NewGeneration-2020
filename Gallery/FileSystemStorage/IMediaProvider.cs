using System;
using System.Threading.Tasks;

namespace FileSystemStorage
{
    public interface IMediaProvider
    {
        bool Upload(byte[] dateBytes, string path);

        byte[] Read(string path);

        bool Delete(string path);
    }
}
