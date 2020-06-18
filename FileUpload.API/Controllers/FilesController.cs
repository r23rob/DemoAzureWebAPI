using System.Collections.Generic;
using System.IO;
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

        /// <summary>
        ///  Returns a List of all files that have been uploaded to Azure
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<File>>> Get()
        {
            logger.LogInformation($"{nameof(FilesController)}: -  Request Type:GET, Request:{nameof(Get)}");

            var result = await fileService.ListFiles();
            return result.ToList();
        }

        /// <summary>
        /// Returns the specified file as a download
        /// </summary>
        [HttpGet("{fileID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<File>> GetByID(int fileID)
        {
            logger.LogInformation($"{nameof(FilesController)}: -  Request Type:GET, Request:{nameof(GetByID)}");
            return await fileService.GetFile(fileID);
        }

        /// <summary>
        /// Uploads a New File to Azure and stores the information in the DB
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> Post(IFormFile formFile)
        {
            logger.LogInformation($"{nameof(FilesController)}: -  Request Type:GET, Request:{nameof(Post)}");

            // TODO Could cause alot of Memory usage use a temp file
            using (var fileStream = new MemoryStream())
            {
                await formFile.CopyToAsync(fileStream);
                await fileService.AddFile(formFile.FileName, formFile.ContentType, fileStream);
            }
            
            return NoContent();
        }
    }
}
