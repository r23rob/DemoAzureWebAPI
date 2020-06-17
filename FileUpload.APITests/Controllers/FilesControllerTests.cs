using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Moq;
using FileUpload.API.Services;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace FileUpload.API.Controllers.Tests
{
    [TestFixture()]
    public class FilesControllerTests
    {
        FilesController controller;
        Mock<IFileService> mockFileService;

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
            mockFileService = new Mock<IFileService>();
            controller = new FilesController(new NullLogger<FilesController>(), mockFileService.Object);

        }

        [Test()]
        public async Task Get_WhenCalled_Returns_FileList()
        {
            //Arrange
            mockFileService
                .Setup(x => x.ListFiles())
                .Returns(Task.FromResult(FileList));
            
            // Act
            var result = await controller.Get();

            // Asset
            var fileList = result.Value;
            Assert.IsInstanceOf<IEnumerable<File>>(fileList);
            Assert.AreEqual(2, fileList.Count());
        }

        [Test()]
        public async Task GetById_WithValidId_ShouldReturnFile()
        {
            // Arrange
            int fileId = 2;
            mockFileService
                .Setup(x => x.GetFile(It.IsAny<int>()))
                .Returns(Task.FromResult(FileList.FirstOrDefault(x => x.FileId == fileId)));

            // Act
            var result = await controller.GetByID(fileId);

            // Asset
            var file = result.Value;
            Assert.IsInstanceOf<File>(file);
            Assert.IsNotNull(file);
            Assert.AreEqual(fileId, file.FileId);
        }

        [Test()]
        public async Task GetById_WithInvalidId_ShouldReturnNotFound()
        {
            // Arrange
            int fileId = 3;

            mockFileService
                .Setup(x => x.GetFile(It.IsAny<int>()))
                .Returns(Task.FromResult<File>(null));

            // Act
            var result = await controller.GetByID(fileId);

            // Asset
            var file = result.Value;
            Assert.IsNull(file);
        }

        [Test()]
        public async Task Post_WithValidFile_ShouldReturnSuccess()
        {
            // Arrange
            var mockFile = new Mock<IFormFile>();
            var fileName = "test.pdf";
            var ms = new MemoryStream(Encoding.UTF8.GetBytes("test file"));
            mockFile.Setup(x => x.OpenReadStream()).Returns(ms);
            mockFile.Setup(x => x.FileName).Returns(fileName);
            mockFile.Setup(x => x.Length).Returns(ms.Length);

            mockFileService
                .Setup(x => x.AddFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Stream>()))
                .Returns(Task.FromResult(true));

            // Act
            var result = await controller.Post(mockFile.Object);

            // Asset
            Assert.IsInstanceOf<NoContentResult>(result);
        }

        [Test()]
        public async Task Post_WithEmptyFile_ShouldReturnSuccess()
        {
            // Arrange
            var mockFile = new Mock<IFormFile>();
            var fileName = "test.pdf";
            MemoryStream ms = null;
            mockFile.Setup(x => x.OpenReadStream()).Returns(ms);
            mockFile.Setup(x => x.FileName).Returns(fileName);

            mockFileService
                .Setup(x => x.AddFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Stream>()))
                .Returns(Task.FromResult(true));

            // Act
            var result = await controller.Post(mockFile.Object);

            // Asset
            Assert.IsInstanceOf<NoContentResult>(result);
        }
    }
}
