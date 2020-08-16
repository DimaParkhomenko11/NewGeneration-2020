using System;
using System.IO;
using System.IO.Abstractions;
using FileSystemStorage.Implementation;
using Moq;
using NSubstitute;
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
            string path = "Correct_path";

            var file = Mock.Of<IFile>();
            Mock.Get(file).Setup(write => write.WriteAllBytes(path, new byte[]{ }));
            Mock.Get(file).Setup(exist => exist.Exists(path)).Returns(true);

            MediaProvider mediaProvider = MakeMediaProvider(file);
            mediaProvider.Upload(new byte[] { }, path);

            Mock.Get(file).Verify(f => f.WriteAllBytes(It.IsAny<string>(),It.IsAny<byte[]>()));
            Mock.Get(file).Verify(f => f.Exists(It.Is<string>(s => s.Contains("Correct_path"))));

        }
/*
        [Test]
        public void Upload_InCorrectPath_ReturnDirectoryNotFoundException()
        {
            string path = "InCorrectPath";

            var file = Mock.Of<IFile>();
            Mock.Get(file).Setup(write => write.WriteAllBytes(path, new byte[] { })).Throws( new Exception("fake exception"));

            MediaProvider mediaProvider = MakeMediaProvider(file);

            Mock.Get(file).Verify(f => f.WriteAllBytes(It.IsAny<string>(), It.IsAny<byte[]>()));

            Mock.Get(file).
        }

        [Test]
        public void Upload_NullPath_ReturnArgumentNullException()
        {
            string path = null;
            byte[] bytes = new byte[] { };

            MediaProvider mediaProvider = MakeMediaProvider();

            Assert.Throws<ArgumentNullException>(() => mediaProvider.Upload(bytes, path));
        }
        
        */
        [Test]
        public void Read_CorrectValue_ReturnTrue()
        {
            string path = "Correct_path";
            var file = Mock.Of<IFile>();
            Mock.Get(file).Setup(f => f.ReadAllBytes(path)).Returns(new byte[] { });

            MediaProvider mediaProvider = MakeMediaProvider(file);
            mediaProvider.Read(path);

            Mock.Get(file).Verify(f => f.ReadAllBytes(It.Is<String>(s => s.Contains("Correct_path"))));

        }

    }
}