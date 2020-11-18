using Microsoft.EntityFrameworkCore;
using System;
using FileUpload.API.Models;

namespace FileUpload.API.Data
{
    public class FilesDbContext : DbContext
    {

        public DbSet<File> Files { get; set; }

        public FilesDbContext(DbContextOptions<FilesDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //seed example projects
            modelBuilder.Entity<File>().HasData(
                 new File()
                 {
                     FileId = 1,
                     FileName = "file1.zip",
                     CreatedDate = DateTime.Now,
                     MimeType = "application/zip"
                 });

            modelBuilder.Entity<File>().HasData(
                new File()
                 {
                     FileId = 2,
                     FileName = "file2.zip",
                     CreatedDate = DateTime.Now,
                     MimeType = "application/zip"
                 });
        }
    }
}
