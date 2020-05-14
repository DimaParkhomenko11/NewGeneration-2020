using System;
using System.IO;
using Gallery.BLL.Interfaces;
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
        public void UploadTest()
        {
            string path =
                "D:\\GitHub\\NewGeneration\\NewGeneration2020\\Gallery\\FileSystemStorage.Tests\\Files\\FileTest.png";
            byte[] bytes = new byte[] { };

            MediaProvider mediaProvider = new MediaProvider();

            Assert.AreEqual(true, mediaProvider.Upload(bytes, path));
        }

        [Test]
        public void UploadFalsePathTest()
        {
            string path =
                "D:\\GitHub\\NewGeneration\\NewGeneration2020\\Gallery\\FileSystemStorage.Tests\\Files\\";
            byte[] bytes = new byte[] { };

            MediaProvider mediaProvider = new MediaProvider();

            Assert.Throws<DirectoryNotFoundException>(() => mediaProvider.Upload(bytes, path));
        }

        [Test]
        public void UploadNullPathTest()
        {
            string path = null;
            byte[] bytes = new byte[] { };

            MediaProvider mediaProvider = new MediaProvider();

            Assert.Throws<ArgumentNullException>(() => mediaProvider.Upload(bytes, path));
        }
    }
}