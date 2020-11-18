using System.Collections.Generic;
using System.Threading.Tasks;
using FileUpload.API.Models;

namespace FileUpload.API.Data
{
    public interface IFileRepository
    {
        Task<bool> AddFile(File file);
        Task<IEnumerable<File>> GetAllFiles();
        Task<File> GetFileById(int fileId);
    }
}