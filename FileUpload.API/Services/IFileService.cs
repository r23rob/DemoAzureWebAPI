using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileUpload.API.Services
{
    public interface IFileService
    {
        public Task AddFile(File file);
        public Task<IEnumerable<File>> ListFiles();
        public Task<File> GetFile(int fileId);
    }
}