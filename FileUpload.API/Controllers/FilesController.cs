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
        private readonly IFileRepository fileRepository;

        public FilesController(ILogger<FilesController> logger, IFileRepository fileRepository)
        {
            this.logger = logger;
            this.fileRepository = fileRepository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<File>>> Get()
        {
            logger.LogInformation($"{nameof(FilesController)}: - GET - List Files");

            var fileList = await fileRepository.GetAllFiles();
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

            var fileResult = await fileRepository.GetFileById(fileID);
            if (fileResult != null)
            {
                return fileResult;
            }
            
            return NotFound($"Unable to find a file with FileId={fileID}");
        }

    }
}
