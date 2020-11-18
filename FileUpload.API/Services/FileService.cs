using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using FileUpload.API.Core.Exceptions;
using System.IO;
using System.Text;
using FileUpload.API.Data;
using File = FileUpload.API.Models.File;

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

        public async Task<bool> AddFile(string fn, string mt, Stream st)
        {
            ValidateFile(fn, mt, st);

            var file = new File(fn, mt);

            using (StreamReader reader = new StreamReader(st, Encoding.UTF8))
            {
                file.FileContent = reader.ReadToEnd();
            }

            // Save Data
            return await fileRepository.AddFile(file);

        }

        private void ValidateFile(string fileName, string mimeType, Stream stream)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new BadRequestException($"{nameof(fileName)} cannot be null");
            }

            if (string.IsNullOrWhiteSpace(mimeType))
            {
                throw new BadRequestException($"{nameof(mimeType)} cannot be null");
            }

            if (!(stream?.Length > 0))
            {
                throw new BadRequestException($"{nameof(stream)} cannot be null");
            }
        }

        public async Task<IEnumerable<File>> ListFiles()
        {
            var fileListResult = await fileRepository.GetAllFiles();
            if (fileListResult == null)
            {
                throw new NotFoundException($"Unable to find any files");
            }

            return fileListResult;
        }

        public async Task<File> GetFile(int fileId)
        {
            var fileResult =  await fileRepository.GetFileById(fileId);
            if (fileResult == null)
            {
                throw new NotFoundException($"Unable to find file matching FileId:{fileId}");
            }

            return fileResult;
        }

    }
}
