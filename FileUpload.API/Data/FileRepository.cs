using System.Collections.Generic;
using System.Threading.Tasks;
using FileUpload.API.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using File = FileUpload.API.Models.File;

namespace FileUpload.API.Data
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
                filesDbContext.Files.Add(file);
                await filesDbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                logger.LogError($"Failed to Save {nameof(File)} to DB");
                return false;
            }
        }
        
        public async Task<IEnumerable<File>> GetAllFiles()
        {
            return await filesDbContext.Files.ToListAsync();
        }

        public async Task<File> GetFileById(int fileId)
        {
            return await filesDbContext.Files.FirstOrDefaultAsync(x => x.FileId == fileId);
        }

    }
}
