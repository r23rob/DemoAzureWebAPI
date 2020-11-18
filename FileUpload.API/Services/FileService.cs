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
        private readonly ILogger<FileService> log;
        private readonly IFileRepository filerepo;

        public FileService(ILogger<FileService> log, IFileRepository fr)
        {
            this.log = log;
            this.filerepo = fr;
        }

        public async Task<bool> AddFile(string fn, string mt, Stream st)
        {
            if (string.IsNullOrWhiteSpace(fn))
            {
                throw new BadRequestException($"{nameof(fn)} cannot be null");
            }

            if (string.IsNullOrWhiteSpace(mt))
            {
                throw new BadRequestException($"{nameof(mt)} cannot be null");
            }

            if (!(st?.Length > 0))
            {
                throw new BadRequestException($"{nameof(st)} cannot be null");
            }

            var file = new File(fn, mt);

            using (StreamReader reader = new StreamReader(st, Encoding.UTF8))
            {
                file.FileContent = reader.ReadToEnd();
            }

            // Save Data
            return await filerepo.AddFile(file);

        }

        public async Task<IEnumerable<File>> ListFiles()
        {
            var fileListResult = await filerepo.GetAllFiles();
            if (fileListResult == null)
            {
                throw new NotFoundException($"Unable to find any files");
            }

            return fileListResult;
        }

        public async Task<File> GetFile(int fId)
        {
            var fileResult =  await filerepo.GetFileById(fId);
            if (fileResult == null)
            {
                throw new NotFoundException($"Unable to find file matching FileId:{fId}");
            }

            return fileResult;
        }

    }
}
