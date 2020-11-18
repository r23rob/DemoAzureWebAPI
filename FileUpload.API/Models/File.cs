using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;

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
            FileContent = System.IO.File.ReadAllText(FileName);
        }

        public int FileId { get; set; }

        public string FileName { get; set; }

        public DateTime CreatedDate { get; set; }
        
        public string MimeType { get; set; }

        [NotMapped]
        public string FileContent { get; set; }
    }    
}
