using FileUpload.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FileUpload.API.Services
{
    public class FileService : IFileService
    {
        private readonly ILogger<FileService> logger;
        private readonly IFileRepository fileRepository;

        public FileService(ILogger<FileService> logger, IFileRepository fileRepository)
        {
            this.logger = logger;
            this.fileRepository = fileRepository;
        }

        public async Task<bool> AddFile(File file)
        {
            if(file == null)
            {
                throw new ArgumentException($"{nameof(File)} cannot be null");
            }

            // ToDo Upload File Azure
            
            // Save Data
            return await fileRepository.AddFile(file);
           
        }

        public async Task<IEnumerable<File>> ListFiles()
        {
            // ToDo Add Paging/Limit results
            return await fileRepository.GetAllFiles();
        }

        public async Task<File> GetFile(int fileId)
        {
            //ToDo Get File From Azure

            return await fileRepository.GetFileById(fileId);
        }

    }
}
