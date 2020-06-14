using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Gallery.BLL.Contract;

namespace Gallery.BLL.Interfaces
{
    public interface IImagesService
    {
        Task<bool> UploadTempImageAsync(byte[] dateBytes, string path, UserDto userDto, string userPathImages);
        Task<bool> UploadImageAsync(byte[] dateBytes, string path, UserDto userDto);

        byte[] ReadFile(string path);

        Task<bool> DeleteFileAsync(string path);
    }
}
