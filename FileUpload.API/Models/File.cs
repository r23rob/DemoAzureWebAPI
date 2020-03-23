using System;

namespace FileUpload.API
{
    public class File
    {
        public int FileId { get; set; }

        public string FileName { get; set; }

        public DateTime CreatedDate { get; set; }

        public string MimeType { get; set; }
    }
}
