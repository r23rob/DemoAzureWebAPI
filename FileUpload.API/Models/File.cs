using System;

namespace FileUpload.API.Models
{
    public class File
    {
        public File()
        {
        }

        public File(string fileName, string mimeType)
        {
            FileName = fileName;
            MimeType = mimeType;
            CreatedDate = DateTime.Now;
        }

        public int FileId { get; set; }

        public string FileName { get; set; }

        public DateTime CreatedDate { get; set; }

        public string MimeType { get; set; }
    }    
}
