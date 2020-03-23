using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FileUpload.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilesController : ControllerBase
    {
        private static readonly List<File> FileList = new List<File>
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

        private readonly ILogger<FilesController> logger;

        public FilesController(ILogger<FilesController> logger)
        {
            this.logger = logger;
        }

        [HttpGet]
        public ActionResult<IEnumerable<File>> Get()
        {
            logger.LogInformation($"{nameof(FilesController)}: - GET - List Files");

            if (FileList?.Any() == true)
            {
                return FileList;
            }

            return NotFound($"No Files Found"); ;
        }

        [HttpGet("{fileID}")]
        public ActionResult<File>Get(int fileID)
        {
            logger.LogInformation($"{nameof(FilesController)}: - GET - Individual File");

            var fileResult = FileList.FirstOrDefault(f => f.FileId == fileID);
            if(fileResult != null)
            {
                return fileResult;
            }
            
            return NotFound($"Unable to find FileId: {fileID}");
        }

    }
}
