using NUnit.Framework;
using FileUpload.API.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace FileUpload.API.Controllers.Tests
{
    [TestFixture()]
    public class FilesControllerTests
    {
        FilesController controller;

        [SetUp]
        public void Setup()
        {
            controller = new FilesController(new NullLogger<FilesController>());
        }

        [Test()]
        public void Get_WhenCalled_Returns_FileList()
        {
            // Act
            var result = controller.Get();

            // Asset
            var fileList = result.Value;
            Assert.IsInstanceOf<IEnumerable<File>>(fileList);
            Assert.AreEqual(2, fileList.Count());
        }

        [Test()]
        public void GetById_WithValidId_ShouldReturnFile()
        {
            // Arrange
            int fileId = 2;

            // Act
            var result = controller.Get(fileId);

            // Asset
            var file = result.Value;
            Assert.IsInstanceOf<File>(file);
            Assert.IsNotNull(file);
            Assert.AreEqual(fileId, file.FileId);
        }

        [Test()]
        public void GetById_WithInvalidId_ShouldReturnNotFound()
        {
            // Arrange
            int fileId = 3;

            // Act
            var result = controller.Get(fileId);

            // Asset
            var file = result.Value;
            Assert.IsNull(file);
        }
    }
}
