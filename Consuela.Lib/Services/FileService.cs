using Consuela.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Consuela.Lib.Services
{
    /// <summary>
    /// Abstraction of any File operations to avoid coupling and allow for Unit Testability.
    /// </summary>
    public class FileService 
        : IFileService
    {
        public void DeleteDirectoryIfExists(string path)
        {
            if(Directory.Exists(path)) Directory.Delete(path, true);
        }

        public void DeleteFileIfExists(FileInfoEntity file)
        {
            if(File.Exists(file.FullName)) File.Delete(file.FullName);
        }

        public List<FileInfoEntity> GetFiles(PathAndPattern target, int daysOld)
        {
            var files = new DirectoryInfo(target.Path)
                .GetFiles(target.Pattern, SearchOption.AllDirectories)
                .Where(x => (DateTime.Now - x.CreationTime).Days > daysOld)
                .Select(x => new FileInfoEntity(x))
                .ToList();

            return files;
        }

        public bool PathContainsFiles(string path)
        {
            var di = new DirectoryInfo(path);

            var hasFiles = di.EnumerateFiles().Any();

            return hasFiles;
        }

        public void CreateDirectory(string path) => Directory.CreateDirectory(path);

        public void AppendAllText(string path, string? contents) => File.AppendAllText(path, contents);
    }
}
