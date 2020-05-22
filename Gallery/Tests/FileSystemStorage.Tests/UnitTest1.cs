using System;
using System.IO;
using Gallery.BLL.Interfaces;
using Gallery.BLL.Services;
using Moq;
using NUnit.Framework;

namespace FileSystemStorage.Tests
{
    [TestFixture]
    public class Tests
    {

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Upload_CorrectValue_ReturnTrue()
        {
            string path =
                "D:\\GitHub\\NewGeneration\\NewGeneration2020\\Gallery\\FileSystemStorage.Tests\\Files\\FileTest.png";
            byte[] bytes = new byte[] { };

            MediaProvider mediaProvider = new MediaProvider();
            var result = mediaProvider.Upload(bytes, path);

            Assert.IsTrue(result);
        }

        [Test]
        public void Upload_InCorrectPath_ReturnDirectoryNotFoundException()
        {
            string path =
                "D:\\GitHub\\NewGeneration\\NewGeneration2020\\Gallery\\FileSystemStorage.Tests\\Files\\";
            byte[] bytes = new byte[] { };

            MediaProvider mediaProvider = new MediaProvider();

            Assert.Throws<DirectoryNotFoundException>(() => mediaProvider.Upload(bytes, path));
        }

        [Test]
        public void Upload_NullPath_ReturnArgumentNullException()
        {
            string path = null;
            byte[] bytes = new byte[] { };

            MediaProvider mediaProvider = new MediaProvider();

            Assert.Throws<ArgumentNullException>(() => mediaProvider.Upload(bytes, path));
        }
    }
}