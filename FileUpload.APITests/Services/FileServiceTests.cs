using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileUpload.API.Core.Exceptions;
using FileUpload.API.Data;
using FileUpload.API.Services;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using NUnit.Framework;
using File = FileUpload.API.Models.File;

namespace FileUpload.APITests.Services
{
    [TestFixture()]
    public class FileServiceTests
    {
        FileService fileService;
        Mock<IFileRepository> mockFileRepository;

        private static readonly IEnumerable<File> FileList = new List<File>
        {
            new File()
            {
               FileId = 1,
               FileName = "file1.zip",
               CreatedDate = DateTime.Now,
               MimeType = "application/zip"
            },
            new File()
            {
                FileId = 2,
                FileName = "file2.zip",
                CreatedDate = DateTime.Now,
                MimeType = "application/zip"
            },
        };

        [SetUp]
        public void Setup()
        {
            mockFileRepository = new Mock<IFileRepository>();
            fileService = new FileService(new NullLogger<FileService>(), mockFileRepository.Object);
        }


        [Test()]
        public async Task AddFile_With_ValidFile_Should_ReturnTrue()
        {
            //Arrange
            mockFileRepository
                .Setup(x => x.AddFile(It.IsAny<File>()))
                .Returns(Task.FromResult(true));
            
            // Act
            var result = await fileService.AddFile("filename.zip", "application/zip", new MemoryStream(Encoding.UTF8.GetBytes("test file")));

            // Asset
            Assert.IsTrue(result);
        }

        [Test()]
        public void AddFile_With_NullFileName_Should_ThrowArgumentException()
        {
            //Arrange
            mockFileRepository
                .Setup(x => x.AddFile(It.IsAny<File>()))
                .Returns(Task.FromResult(false));

            // Act/Asset
            Assert.ThrowsAsync<BadRequestException>(
                async () => await fileService.AddFile(null, "application/zip", new MemoryStream(Encoding.UTF8.GetBytes("test file")))
                );
        }

        [Test()]
        public void AddFile_With_NullMimeType_Should_ThrowArgumentException()
        {
            //Arrange
            mockFileRepository
                .Setup(x => x.AddFile(It.IsAny<File>()))
                .Returns(Task.FromResult(false));

            // Act/Asset
            Assert.ThrowsAsync<BadRequestException>(async () => await fileService.AddFile(null, "application/zip", new MemoryStream(Encoding.UTF8.GetBytes("test file"))));
        }

        [Test()]
        public void AddFile_With_NullFile_Should_ThrowArgumentException()
        {
            //Arrange
            mockFileRepository
                .Setup(x => x.AddFile(It.IsAny<File>()))
                .Returns(Task.FromResult(false));

            // Act/Asset
            Assert.ThrowsAsync<BadRequestException>(async () => await fileService.AddFile("filename.zip", "application/zip", null));
        }

        [Test()]
        public async Task ListFiles_Called_Should_Return()
        {
            //Arrange
            mockFileRepository
                .Setup(x => x.GetAllFiles())
                .Returns(Task.FromResult(FileList));

            // Act
            var result = await fileService.ListFiles();

            // Asset
            Assert.IsInstanceOf<IEnumerable<File>>(result);
            Assert.AreEqual(2, result.Count());
        }

        [Test()]
        public async Task GetFile_With_ValidId_Should_ReturnFile()
        {
            // Arrange
            int fileId = 2;
            mockFileRepository
                .Setup(x => x.GetFileById(It.IsAny<int>()))
                .Returns(Task.FromResult(FileList.FirstOrDefault(x => x.FileId == fileId)));

            // Act
            var result = await fileService.GetFile(fileId);

            // Asset
            Assert.IsInstanceOf<File>(result);
            Assert.IsNotNull(result);
            Assert.AreEqual(fileId, result.FileId);
        }

        [Test()]
        public void GetFile_With_InvalidId_Should_ReturnFile()
        {
            // Arrange
            int fileId = 3;
            mockFileRepository
                .Setup(x => x.GetFileById(It.IsAny<int>()))
                .Returns(Task.FromResult(FileList.FirstOrDefault(x => x.FileId == fileId)));

            // Act/Asset
            Assert.ThrowsAsync<NotFoundException>(async () => await fileService.GetFile(fileId));
        }
    }
}