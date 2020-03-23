using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileUpload.API.Core.Exceptions;
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
            logger.LogInformation($"{nameof(FilesController)}: -  Request Type:GET, Request:{nameof(Get)}");

            var result = await fileService.ListFiles();
            return result.ToList();
        }

        [HttpGet("{fileID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<File>> GetByID(int fileID)
        {
            logger.LogInformation($"{nameof(FilesController)}: -  Request Type:GET, Request:{nameof(GetByID)}");
            return await fileService.GetFile(fileID);
        }
    }
}
