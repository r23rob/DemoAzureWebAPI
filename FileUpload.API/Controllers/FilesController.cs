using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FileUpload.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using File = FileUpload.API.Models.File;

namespace FileUpload.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilesController : ControllerBase
    {

        private readonly ILogger<FilesController> log;
        private readonly IFileService fs;

        public FilesController(ILogger<FilesController> log, IFileService fs)
        {
            this.log = log;
            this.fs = fs;
        }

        /// <summary>
        ///  Returns a List of all files that have been uploaded
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<File>>> Get()
        {
            log.LogInformation($"{nameof(FilesController)}: -  Request Type:GET, Request:{nameof(Get)}");

            var result = await fs.ListFiles();
            return result.ToList();
        }

        /// <summary>
        /// Returns the specified file details
        /// </summary>
        [HttpGet("{fileid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<File>> GetByID(int fileid)
        {
            log.LogInformation($"{nameof(FilesController)}: -  Request Type:GET, Request:{nameof(GetByID)}");
            return await fs.GetFile(fileid);
        }

        /// <summary>
        /// Inserts a New File to the database
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> Post(IFormFile formFile)
        {
            log.LogInformation($"{nameof(FilesController)}: -  Request Type:GET, Request:{nameof(Post)}");

            var fileStream = new MemoryStream();
            await formFile.CopyToAsync(fileStream);
            await fs.AddFile(formFile.FileName, formFile.ContentType, fileStream);
            
            
            return NoContent();
        }
    }
}
