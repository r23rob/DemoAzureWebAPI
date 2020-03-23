using FileUpload.API.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FileUpload.API.Services
{
    public class FileRepository : IFileRepository
    {

        private readonly FilesDbContext filesDbContext;

        public FileRepository(FilesDbContext filesDbContext)
        {
            this.filesDbContext = filesDbContext;
        }

        public async Task AddFile(File file)
        {
            // Add And Save File

            // ToDo catch exception and log
            filesDbContext.Files.Add(file);
            await filesDbContext.SaveChangesAsync();
        }


        public async Task<IEnumerable<File>> GetAllFiles()
        {
            // ToDo Add Paging/Limit results
            return await filesDbContext.Files.ToListAsync();
        }

        public async Task<File> GetFileById(int fileId)
        {
            // ToDo Get File From Azure

            return await filesDbContext.Files.FirstOrDefaultAsync(x => x.FileId == fileId);
        }

    }
}
