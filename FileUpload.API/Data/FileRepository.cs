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
    public class FileRepository : IFileRepository
    {
        private readonly ILogger<FileService> logger;
        private readonly FilesDbContext filesDbContext;

        public FileRepository(ILogger<FileService> logger, FilesDbContext filesDbContext)
        {
            this.logger = logger;
            this.filesDbContext = filesDbContext;
        }

        public async Task<bool> AddFile(File file)
        {
            try
            {
                // ToDo catch exception and log
                filesDbContext.Files.Add(file);
                await filesDbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                // Todo Log actual Exception/stack trace
                logger.LogError($"Failed to Save {nameof(File)} to DB");
                return false;
            }
        }
        
        public async Task<IEnumerable<File>> GetAllFiles()
        {
            // ToDo Add Paging/Limit results
            return await filesDbContext.Files.ToListAsync();
        }

        public async Task<File> GetFileById(int fileId)
        {
            return await filesDbContext.Files.FirstOrDefaultAsync(x => x.FileId == fileId);
        }

    }
}
