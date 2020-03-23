using NUnit.Framework;
using FileUpload.API.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Moq;
using FileUpload.API.Services;
using System.Threading.Tasks;

namespace FileUpload.API.Controllers.Tests
{
    [TestFixture()]
    public class FilesControllerTests
    {
        FilesController controller;
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
            controller = new FilesController(new NullLogger<FilesController>(), mockFileRepository.Object);

        }

        [Test()]
        public async Task Get_WhenCalled_Returns_FileList()
        {
            //Arange
            mockFileRepository
                .Setup(x => x.GetAllFiles())
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
            mockFileRepository
                .Setup(x => x.GetFileById(It.IsAny<int>()))
                .Returns(Task.FromResult(FileList.FirstOrDefault(x => x.FileId == fileId)));

            // Act
            var result = await controller.Get(fileId);

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
            
            mockFileRepository
                .Setup(x => x.GetFileById(It.IsAny<int>()))
                .Returns(Task.FromResult<File>(null));

            // Act
            var result = await controller.Get(fileId);

            // Asset
            var file = result.Value;
            Assert.IsNull(file);
        }
    }
}
