using System;
using System.IO;
using Moq;
using NUnit.Framework;

namespace FileSystemStorage.Tests
{
    [TestFixture]
    public class MediaProviderTests
    {
        
        private MediaProvider MakeMediaProvider()
        {
            return new MediaProvider();
        }

        [Test]
        public void Upload_CorrectValue_ReturnTrue()
        {
            string path =
                @"D:\GitHub\NewGeneration2020\Gallery\Testing\FileSystemStorage.Testing\UploadTestingFiles\TestFile.png";
            byte[] bytes = new byte[]{};

            MediaProvider mediaProvider = MakeMediaProvider();

            var result = mediaProvider.Upload(bytes, path);

            Assert.IsTrue(result);
        }

        [Test]
        public void Upload_InCorrectPath_ReturnDirectoryNotFoundException()
        {
            string path =
                "D:\\GitHub\\NewGeneration\\NewGeneration2020\\Gallery\\FileSystemStorage.Tests\\Files\\";
            byte[] bytes = new byte[] { };

            MediaProvider mediaProvider = MakeMediaProvider();

            Assert.Throws<DirectoryNotFoundException>(() => mediaProvider.Upload(bytes, path));
        }

        [Test]
        public void Upload_NullPath_ReturnArgumentNullException()
        {
            string path = null;
            byte[] bytes = new byte[] { };

            MediaProvider mediaProvider = MakeMediaProvider();

            Assert.Throws<ArgumentNullException>(() => mediaProvider.Upload(bytes, path));
        }
    }
}