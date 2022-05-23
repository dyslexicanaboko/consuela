using Consuela.Entity;
using System.Collections.Generic;

namespace Consuela.Lib.Services
{
    public interface IFileService
    {
        List<FileInfoEntity> GetFiles(PathAndPattern target, int daysOld);
        
        bool PathContainsFiles(string path);

        void DeleteFileIfExists(FileInfoEntity file);

        void DeleteDirectoryIfExists(string path);

        void CreateDirectory(string path);

        void AppendAllText(string path, string? contents);
    }
}