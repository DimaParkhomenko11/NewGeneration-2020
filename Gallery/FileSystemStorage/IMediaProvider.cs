using System;
using System.Threading.Tasks;

namespace FileSystemStorage
{
    public interface IMediaProvider
    {
        bool Upload(byte[] dateBytes, string path);
    }
}
