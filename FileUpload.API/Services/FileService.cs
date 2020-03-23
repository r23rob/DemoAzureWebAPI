using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FileUpload.API.Core.Exceptions;

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
                throw new BadRequestException($"{nameof(File)} cannot be null");
            }

            // ToDo Upload File Azure
            
            // Save Data
            return await fileRepository.AddFile(file);
           
        }

        public async Task<IEnumerable<File>> ListFiles()
        {
            // ToDo Add Paging/Limit results
            var fileListResult = await fileRepository.GetAllFiles();
            if (fileListResult == null)
            {
                throw new NotFoundException($"Unable to find any files");
            }

            return fileListResult;
        }

        public async Task<File> GetFile(int fileId)
        {
            //ToDo Get File From Azure
            var fileResult =  await fileRepository.GetFileById(fileId);
            if (fileResult == null)
            {
                throw new NotFoundException($"Unable to find file matching FileId:{fileId}");
            }

            return fileResult;
        }

    }
}
