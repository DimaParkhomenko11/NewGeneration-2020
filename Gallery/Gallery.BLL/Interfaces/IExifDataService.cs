using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gallery.BLL.Interfaces
{
    public interface IExifDataService
    {
        string GetTitle(string loadExifPath);
        string GetDateUpload(string loadExifPath);
        string GetFileSize(string loadExifPath);
        string GetDateCreation(string loadExifPath);
        string GetCameraManufacturer(string loadExifPath);
        string GetModelOfCamera(string loadExifPath);
        
    }
}
