using NUnit.Framework;
using FileUpload.API.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging.Abstractions;
using System.Linq;

namespace FileUpload.API.Services.Tests
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
            //Arange
            mockFileRepository
                .Setup(x => x.AddFile(It.IsAny<File>()))
                .Returns(Task.FromResult(true));
            
            var fileToAdd = new File()
            {
                FileId = 2,
                FileName = "filename.zip"
            };
            
            // Act
            var result = await fileService.AddFile(fileToAdd);

            // Asset
            Assert.IsTrue(result);
        }

        [Test()]
        public void AddFile_With_NullFile_Should_ThrowArgumentException()
        {
            //Arange
            mockFileRepository
                .Setup(x => x.AddFile(It.IsAny<File>()))
                .Returns(Task.FromResult(false));

            // Act/Asset
            Assert.ThrowsAsync<ArgumentException>(async () => await fileService.AddFile(null));
        }

        [Test()]
        public async Task ListFiles_Called_Should_Return()
        {
            //Arange
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
            // Arange
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
        public async Task GetFile_With_InvalidId_Should_ReturnFile()
        {
            // Arange
            int fileId = 3;
            mockFileRepository
                .Setup(x => x.GetFileById(It.IsAny<int>()))
                .Returns(Task.FromResult(FileList.FirstOrDefault(x => x.FileId == fileId)));

            // Act
            var result = await fileService.GetFile(fileId);

            // Asset
            Assert.IsNull(result);
        }
    }
}