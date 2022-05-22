using Consuela.Entity;
using System.Collections.Generic;

namespace Consuela.Lib.Services
{
    public interface IFileService
    {
        List<FileInfoEntity> GetFiles(PathAndPattern target, int daysOld);
        
        bool PathContainsFiles(string path);

        void DeleteFile(FileInfoEntity file);

        void DeleteDirectory(string path);

        void AppendAllText(string path, string? contents);
    }
}