using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileUpload.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FileUpload.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilesController : ControllerBase
    {
        
        private readonly ILogger<FilesController> logger;
        private readonly IFileService fileService;

        public FilesController(ILogger<FilesController> logger, IFileService fileService)
        {
            this.logger = logger;
            this.fileService = fileService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<File>>> Get()
        {
            logger.LogInformation($"{nameof(FilesController)}: - GET - List Files");

            var fileList = await fileService.ListFiles();
            if (fileList?.Any() == true)
            {
                return fileList.ToList();
            }

            return NotFound($"No Files Found"); ;
        }

        [HttpGet("{fileID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<File>> Get(int fileID)
        {
            logger.LogInformation($"{nameof(FilesController)}: - GET - Individual File");

            var fileResult = await fileService.GetFile(fileID);
            if (fileResult != null)
            {
                return fileResult;
            }
            
            return NotFound($"Unable to find a file with FileId={fileID}");
        }

    }
}
