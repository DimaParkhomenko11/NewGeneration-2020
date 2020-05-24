using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Gallery.BLL.Interfaces
{
    public interface IImagesService
    {
        bool CompareBitmapsFast(Bitmap bmp1, Bitmap bmp2);

        Task<bool> UploadImageAsync(byte[] dateBytes, string path);

        byte[] ReadFile(string path);

        Task<bool> DeleteFileAsync(string path);

        string NameCleaner(string fileName);
    }
}
