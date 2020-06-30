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
        Task<bool> UploadImageAsync(byte[] dateBytes, string path, int userId);

        Task UploadTempToUserDirectory(MessageDto messageDto);

        Task UpdateTemporaryMediaAsync(string uniqueName);

        Task<bool> MoveFileAsync(string pathSave, string pathRead, int userId);

        byte[] ReadFile(string path);

        Task<bool> DeleteFileAsync(string path);
    }
}
