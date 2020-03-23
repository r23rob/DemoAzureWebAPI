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

        private readonly FilesDbContext filesDbContext;

        public FileService(FilesDbContext filesDbContext)
        {
            this.filesDbContext = filesDbContext;
        }

        public async Task AddFile(File file)
        {
            // ToDo Upload File Azure


            // Save Data
            filesDbContext.Files.Add(file);
            await filesDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<File>> ListFiles()
        {
            // ToDo Add Paging/Limit results
            return await filesDbContext.Files.ToListAsync();
        }

        public async Task<File> GetFile(int fileId)
        {
            return await filesDbContext.Files.FirstOrDefaultAsync(x => x.FileId == fileId);
        }

    }
}
