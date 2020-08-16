using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using FileSystemStorage.Interfaces;

namespace FileSystemStorage.Implementation
{
    public class MediaProvider : IMediaProvider
    {
        public bool Upload(byte[] dateBytes, string path)
        {
            if (dateBytes == null)
                throw new ArgumentNullException(nameof(dateBytes));
            if (path == null)
                throw new ArgumentNullException(nameof(path));
            if (string.IsNullOrWhiteSpace(path))
                throw new ArgumentException("Argument_EmptyPath", nameof(path));

            File.WriteAllBytes(path, dateBytes);
            return File.Exists(path);
        }

        public byte[] Read(string path)
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));
            if (string.IsNullOrWhiteSpace(path))
                throw new ArgumentException("Argument_EmptyPath", nameof(path));

            return File.ReadAllBytes(path);
        }

        public bool Delete(string path)
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));
            if (string.IsNullOrWhiteSpace(path))
                throw new ArgumentException("Argument_EmptyPath", nameof(path));

            File.Delete(path);
            return File.Exists(path);
        }
    }
}
