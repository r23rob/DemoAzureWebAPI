using FileUpload.API.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FileUpload.API.Services
{
    public class FileService : IFileService
    {

        private readonly IFileRepository fileRepository;

        public FileService(IFileRepository fileRepository)
        {
            this.fileRepository = fileRepository;
        }

        public async Task AddFile(File file)
        {
            // ToDo Upload File Azure


            // Save Data
            await fileRepository.AddFile(file);
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
