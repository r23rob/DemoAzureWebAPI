﻿using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using File = FileUpload.API.Models.File;

namespace FileUpload.API.Services
{
    public interface IFileService
    {
        public Task<bool> AddFile(string fileName, string mimeType, Stream fileStream);
        public Task<IEnumerable<File>> ListFiles();
        public Task<File> GetFile(int fileId);
    }
}