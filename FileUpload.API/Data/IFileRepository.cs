using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileUpload.API.Services
{
    public interface IFileRepository
    {
        Task<bool> AddFile(File file);
        Task<IEnumerable<File>> GetAllFiles();
        Task<File> GetFileById(int fileId);
    }
}