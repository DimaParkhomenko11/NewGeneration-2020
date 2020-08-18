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

        private MediaProvider MakeMediaProvider(IFile file)
        {
            return new MediaProvider(file);
        }

        [Test]
        public void Upload_CorrectValue_ReturnTrue()
        {
            string path = "Correct_path";
            byte[] bytes = new byte[] { };
            var file = Mock.Of<IFile>();
            Mock.Get(file).Setup(exist => exist.Exists(path)).Returns(true);

            MediaProvider mediaProvider = MakeMediaProvider(file);
            var resultUpload = mediaProvider.Upload(new byte[] { }, path);

            Mock.Get(file)
                .Verify(f => f.WriteAllBytes(It.Is<string>(s => s.Equals("Correct_path")), It.IsAny<byte[]>()));
            Mock.Get(file).Verify(f => f.Exists(It.Is<string>(s => s.Equals("Correct_path"))));
            Assert.True(resultUpload);
        }

        [Test]
        public void Upload_NullPath_ReturnArgumentException()
        {
            string path = null;
            byte[] bytes = new byte[] { };
            var file = Mock.Of<IFile>();

            MediaProvider mediaProvider = MakeMediaProvider(file);

            Assert.Throws<ArgumentException>(() => mediaProvider.Upload(bytes, path));
        }

        [Test]
        public void Upload_InCorrectPath_ReturnFileNotFoundException()
        {
            string path = "InCorrectPath";
            byte[] bytes = new byte[] { };

            var file = Mock.Of<IFile>();
            Mock.Get(file).Setup(f => f.WriteAllBytes(path, bytes)).Throws(new FileNotFoundException());
            MediaProvider mediaProvider = MakeMediaProvider(file);

            Assert.Throws<FileNotFoundException>((() => mediaProvider.Upload(bytes, path)));

        }

        [Test]
        public void Upload_NullBytes_ReturnArgumentNullException()
        {
            string path = "CorrectPath";
            byte[] bytes = null;

            var file = Mock.Of<IFile>();
            MediaProvider mediaProvider = MakeMediaProvider(file);

            Assert.Throws<ArgumentNullException>(() => mediaProvider.Upload(bytes, path));
        }

        [Test]
        public void Upload_SpaceInPath_ReturnArgumentException()
        {
            string path = "  ";
            byte[] bytes = new byte[] { };

            var file = Mock.Of<IFile>();
            MediaProvider mediaProvider = MakeMediaProvider(file);

            Assert.Throws<ArgumentException>(() => mediaProvider.Upload(bytes, path));
        }


        [Test]
        public void Read_CorrectValue_ReturnByteArray()
        {
            string path = "Correct_path";
            var file = Mock.Of<IFile>();
            Mock.Get(file).Setup(f => f.ReadAllBytes(path)).Returns(new byte[] { });

            MediaProvider mediaProvider = MakeMediaProvider(file);
            var result = mediaProvider.Read(path);
            var expected = new byte[] { };
            Mock.Get(file).Verify(f => f.ReadAllBytes(It.Is<String>(s => s.Equals("Correct_path"))));

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Read_NullValue_ReturnArgumentException()
        {
            string path = null;

            var file = Mock.Of<IFile>();
            MediaProvider mediaProvider = MakeMediaProvider(file);

            Assert.Throws<ArgumentException>(() => mediaProvider.Read(path));
        }

        [Test]
        public void Read_InCorrectValue_ReturnFileNotFoundException()
        {
            string path = "IncorrectPath";

            var file = Mock.Of<IFile>();
            Mock.Get(file).Setup(f => f.ReadAllBytes(path)).Throws(new FileNotFoundException());
            MediaProvider mediaProvider = MakeMediaProvider(file);

            Assert.Throws<FileNotFoundException>((() => mediaProvider.Read(path)));
        }

        [Test]
        public void Read_SpaceInPath_ReturnArgumentException()
        {
            string path = "  ";

            var file = Mock.Of<IFile>();
            MediaProvider mediaProvider = MakeMediaProvider(file);

            Assert.Throws<ArgumentException>(() => mediaProvider.Read(path));
        }


        [Test]
        public void Delete_CorrectValue_ReturnTrue()
        {
            string path = "CorrectValue";
            var file = Mock.Of<IFile>();
            Mock.Get(file).Setup(f => f.Exists(path)).Returns(true);
            Mock.Get(file).Setup(f => f.Exists(path)).Returns(false);

            MediaProvider mediaProvider = MakeMediaProvider(file);
            var result = mediaProvider.Delete(path);

            Mock.Get(file).Verify(f => f.Delete(It.Is<string>(s => s.Equals("CorrectValue"))));
            Mock.Get(file).Verify(f => f.Exists(It.Is<string>(s => s.Equals("CorrectValue"))));
            Assert.True(result);
        }

        [Test]
        public void Delete_NullValue_ReturnArgumentException()
        {
            string path = null;
            var file = Mock.Of<IFile>();

            MediaProvider mediaProvider = MakeMediaProvider(file);

            Assert.Throws<ArgumentException>((() => mediaProvider.Delete(path)));
        }

        [Test]
        public void Delete_SpaceInValue_ReturnArgumentException()
        {
            string path = "  ";
            var file = Mock.Of<IFile>();

            MediaProvider mediaProvider = MakeMediaProvider(file);

            Assert.Throws<ArgumentException>((() => mediaProvider.Delete(path)));
        }

        [Test]
        public void Delete_InCorrectValue_ReturnFileNotFoundException()
        {
            string path = "InCorrectValue";
            var file = Mock.Of<IFile>();

            MediaProvider mediaProvider = MakeMediaProvider(file);

            Assert.Throws<FileNotFoundException>((() => mediaProvider.Delete(path)));
        }
    }
}